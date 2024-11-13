using Repository.Helpers;
using Repository.Models;
using Repository.Repositories;
using Service.Interfaces;
using Service.Models;
using Service.Models.Momo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZaloPay.Helper.Crypto;
using ZaloPay.Helper;
using Newtonsoft.Json;
using Service.Models.Transaction;
using Repository.Interfaces;
using Repository.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FirebaseAdmin.Messaging;
using Net.payOS;
using AutoMapper;

namespace Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        private readonly PayOS _payOs;

        public TransactionService(IUnitOfWork unitOfWork, IConfiguration configuration, ITicketService ticketService, PayOS payOs, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _ticketService = ticketService;
            _payOs = payOs;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> CreatePaymentUrl(TransactionRequestModel requestModel, string email)
        {
            // Check if the user performing the transaction exists
            User? existingUser = await _unitOfWork.Users.GetUserByEmail(email);
            if (existingUser == null)
            {
                throw new CustomException(ResponseMessage.UserNotFound + ResponseMessage.FromRequestModel);
            }

            var items = new Dictionary<string, string>();

            switch (requestModel.TransactionType)
            {
                case TransactionType.Ticket:
                    {
                        Order order = await CreateOrder(existingUser, requestModel);
                        items.Add("transId", order.LongOrderId.ToString());
                        items.Add("amount", order.TotalAmount.ToString());
                        break;
                    }
                case TransactionType.Subscription:
                    {
                        Transaction transaction = await CreateTransaction(existingUser, requestModel);
                        var totalAmount = await HandleSubscriptionTransaction(transaction, requestModel.Subscriptions);
                        items.Add("transId", transaction.LongTransactionId.ToString());
                        items.Add("amount", totalAmount.ToString());
                        break;
                    }
                case TransactionType.Promotion:
                    {
                        await CanPurchasePromotion(requestModel.Promotions);
                        Transaction transaction = await CreateTransaction(existingUser, requestModel);
                        var totalAmount = await HandlePromotionTransaction(transaction, requestModel.Promotions);
                        items.Add("transId", transaction.LongTransactionId.ToString());
                        items.Add("amount", totalAmount.ToString());
                        break;
                    }
                case TransactionType.Commission:
                    break;
                default:
                    throw new CustomException(ResponseMessage.TransactionTypeNotFound);
            }

            var param = GenerateZaloPayParameters(items);
            var result = await _payOs.createPaymentLink(param);

            return ApiResponse<Object>.SuccessResponse(result.checkoutUrl, ResponseMessage.PaymentLinkCreateSuccess);
        }

        private async Task CanPurchasePromotion(PromotionRequestModel? promotions)
        {
            if (promotions == null) throw new CustomException($"nameof(promotions) can't be null");
            var existingWorkshop = await GetWorkshop(promotions.WorkshopId ?? Guid.NewGuid());
            DateTime workshopStartDate = existingWorkshop.StartTime ?? throw new CustomException(nameof(workshopStartDate) + "can't be null");
            DateTime workshopEndDate = existingWorkshop.EndTime ?? throw new CustomException(nameof(workshopEndDate) + "can't be null");
            string promotionType = PromotionConstants.GetType(promotions.PromotionType ?? throw new CustomException(nameof(promotionType) + "can't be null"));

            int overlappingPromotions = _unitOfWork.Promotions.CountOverlappingPromotions(promotionType, workshopStartDate, workshopEndDate);
            int numberOfPromotionType = _unitOfWork.Promotions.CountNumberOfPromotionType(existingWorkshop.WorkshopId);

            if (numberOfPromotionType >= 1)
            {
                throw new CustomException("The number of promotions for your workshop cannot exceed 1.");
            }

            switch (promotionType)
            {
                case PromotionConstants.Banner:
                    if (overlappingPromotions >= PromotionConstants.PromotionQuantityMaxLength.BannerLenght)
                        throw new CustomException($"Maximum number of overlapping {promotionType} promotions reached.");
                    return;
                case PromotionConstants.Featured:
                    if (overlappingPromotions >= PromotionConstants.PromotionQuantityMaxLength.FeaturedLenght)
                        throw new CustomException($"Maximum number of overlapping {promotionType} promotions reached.");
                    return;
                case PromotionConstants.Highlight:
                    if (overlappingPromotions >= PromotionConstants.PromotionQuantityMaxLength.FeaturedLenght)
                        throw new CustomException($"Maximum number of overlapping {promotionType} promotions reached.");
                    return;
                default:
                    return;
            }
        }

        private async Task<decimal> HandlePromotionTransaction(Transaction transaction, PromotionRequestModel? promotions)
        {
            if (promotions == null)
                throw new CustomException(ResponseMessage.InvalidInput + "[promotion model]");

            var existingWorkshop = await GetWorkshop(promotions.WorkshopId ?? Guid.NewGuid());
            var promotionPrice = PurchasePromotion(existingWorkshop, promotions.PromotionType);

            //Create Promotion Transaction
            var promotionTransaction = new PromotionTransaction()
            {
                PromotionTransactionId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                PromotionType = promotions.PromotionType,
                UpdatedAt = DateTime.UtcNow,
                Workshop = existingWorkshop,
            };
            //Assign promotion amount

            transaction.Amount = promotionPrice;
            transaction.PromotionTransactions.Add(promotionTransaction);

            await _unitOfWork.Transactions.Update(transaction);

            return promotionPrice;
        }

        private decimal PurchasePromotion(Workshop existingWorkshop, string? promotionType)
        {
            DateTime workshopStartDate = existingWorkshop.StartTime
                ?? throw new CustomException("Start time of workshop can't be null");
            DateTime workshopEndDate = existingWorkshop.EndTime
                    ?? throw new CustomException("End time of workshop can't be null");
            decimal basePrice = PromotionConstants.GetPromotionPrice(promotionType
                ?? throw new CustomException($"{nameof(promotionType)} can't be null"));

            int overlappingPromotions = _unitOfWork.Promotions.CountOverlappingPromotions(promotionType, workshopStartDate, workshopEndDate);
            decimal finalPrice = basePrice * (1 + (PromotionConstants.PromotionBasePrice.DemandFactorIncrease * overlappingPromotions));
            finalPrice = Math.Round(finalPrice, 2, MidpointRounding.AwayFromZero);

            return finalPrice;
        }

        private async Task<decimal> HandleSubscriptionTransaction(Transaction transaction, SubscriptionRequestModel? subscriptions)
        {
            if (subscriptions == null)
                throw new CustomException(ResponseMessage.SubscriptionNotFound + ResponseMessage.FromRequestModel);
            subscriptions.Validate();

            var totalAmount = SubscriptionTiers.GetParticipantPrice(subscriptions.Tier);
            transaction.Amount = totalAmount;
            var subscriptionTransaction = new SubscriptionTransaction()
            {
                SubscriptionTransactionId = Guid.NewGuid(),
                Transaction = transaction,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            transaction.SubscriptionTransactions.Add(subscriptionTransaction);
            await _unitOfWork.Transactions.Update(transaction);

            return totalAmount;
        }

        private async Task<Transaction> CreateTransaction(User existingUser, TransactionRequestModel requestModel)
        {
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                UserId = existingUser.UserId,
                User = existingUser,
                TransactionType = requestModel.TransactionType,
                CurrencyCode = "VND",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            transaction.LongTransactionId = ConvertGuidToLong(transaction.TransactionId);
            await _unitOfWork.Transactions.Add(transaction);
            return transaction;
        }

        private async Task<Order> CreateOrder(User existingUser, TransactionRequestModel requestModel)
        {
            // Assign the existing user's ID to the ParticipantId in the request model
            requestModel.ParticipantId = existingUser.UserId;

            // Create a list to store new OrderDetail models
            List<OrderDetail> updateOrderDetailModels = new List<OrderDetail>();

            // Process each order from the request model
            foreach (var orderDetails in requestModel.Orders)
            {
                ValidateOrderDetails(orderDetails);

                var existingWorkshop = await GetWorkshop(orderDetails.WorkshopId);
                var ticketRank = GetTicketRank(existingWorkshop, orderDetails.TicketRankId ?? Guid.NewGuid());

                // Create a new OrderDetail object based on the retrieved workshop and ticket rank
                OrderDetail newOrderDetails = new OrderDetail()
                {
                    OrderDetailsId = Guid.NewGuid(),
                    Price = ticketRank.Price,
                    CurrencyCode = CurrencyCode.VietnameseCurrency,
                    Quantity = orderDetails.Quantity ?? 1,
                    TotalPrice = ticketRank.Price * (orderDetails.Quantity ?? 1),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Workshop = existingWorkshop,
                };

                // Check ticket quantity and handle ticket creation
                if (newOrderDetails.Quantity == 1)
                {
                    // Create a single ticket and assign it to the OrderDetail
                    Ticket newTicket = new Ticket()
                    {
                        TicketId = Guid.NewGuid(),
                        TicketRank = ticketRank,
                        Workshop = existingWorkshop,
                        CurrencyCode = CurrencyCode.VietnameseCurrency,
                        Price = ticketRank.Price
                    };
                    newOrderDetails.Ticket = newTicket;
                }
                else if (newOrderDetails.Quantity > 1)
                {
                    // Create multiple tickets and assign them to the OrderDetail
                    List<Ticket> ticketList = new List<Ticket>();
                    for (int i = 0; i < newOrderDetails.Quantity; i++)
                    {
                        Ticket newTicket = new Ticket()
                        {
                            TicketId = Guid.NewGuid(),
                            TicketRank = ticketRank,
                            Workshop = existingWorkshop,
                            CurrencyCode = CurrencyCode.VietnameseCurrency,
                            Price = newOrderDetails.Price,
                        };
                        ticketList.Add(newTicket);
                    }
                    newOrderDetails.Tickets = ticketList;
                }
                // Add the new order details to the list
                updateOrderDetailModels.Add(newOrderDetails);
            }

            // Create a new Order and populate its details
            Order order = new Order()
            {
                OrderId = Guid.NewGuid(),
                TotalAmount = updateOrderDetailModels.Sum(od => od.TotalPrice),
                CurrencyCode = "VND",
                PaymentStatus = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PaymentTime = null,
                Participant = existingUser,
                OrderDetails = updateOrderDetailModels,
            };
            order.LongOrderId = ConvertGuidToLong(order.OrderId);
            await _unitOfWork.Orders.Add(order);

            return order;
        }

        private int GetWorkshopCapacity(Workshop existingWorkshop, OrderRequestModel orderRequestModel)
        {
            var curSize = existingWorkshop.Capacity;
            if (orderRequestModel.Quantity == null || curSize - orderRequestModel.Quantity < 0)
            {
                throw new CustomException(ResponseMessage.WorkshopCapacityExceeded);
            }
            return (int)orderRequestModel.Quantity;
        }

        private TicketRank GetTicketRank(Workshop workshop, Guid ticketRankId)
        {
            var ticketRank = workshop.TicketRanks.FirstOrDefault(tr => tr.TicketRankId == ticketRankId);
            if (ticketRank == null)
            {
                throw new CustomException(ResponseMessage.TicketRankNotFound + ResponseMessage.FromWorkshopPickedTicketNotFound);
            }
            return ticketRank;
        }

        private async Task<Workshop> GetWorkshop(Guid workshopId)
        {
            var workshop = await _unitOfWork.Workshops.Get()
                .Include(w => w.TicketRanks)
                .FirstOrDefaultAsync(w => w.WorkshopId == workshopId);

            if (workshop == null)
            {
                throw new CustomException(ResponseMessage.WorkshopNotFound + ResponseMessage.FromWorkshopPickedTicketNotFound);
            }
            return workshop;
        }

        private void ValidateOrderDetails(OrderRequestModel? orderDetails)
        {
            if (orderDetails == null || orderDetails.WorkshopId == null || orderDetails.TicketRankId == null)
            {
                throw new CustomException(ResponseMessage.InvalidCreateRequest + ResponseMessage.FromWorkshopPickedTicketNotFound);
            }
        }

        private Net.payOS.Types.PaymentData GenerateZaloPayParameters(Dictionary<string, string> itemsModel)
        {
            string transId = itemsModel["transId"];
            string amount = itemsModel["amount"];
            string formattedAmount = amount.Substring(0, amount.Length - 3);

            var items = new
            {
                transId = long.Parse(transId),
                amount = int.Parse(formattedAmount),
            };

            var serverName = _configuration["FeServerName"] ?? throw new CustomException(ResponseMessage.EnvVaribaleNotFound);
            var returnUrl = serverName + _configuration["PaymentEnvironment:RETURN_URL"] ?? throw new CustomException(ResponseMessage.EnvVaribaleNotFound);
            var cancelUrl = serverName + _configuration["PaymentEnvironment:CANCELURL"] ?? throw new CustomException(ResponseMessage.EnvVaribaleNotFound);

            long expiredAt = (long)(DateTime.UtcNow.AddMinutes(10) - new DateTime(1970, 1, 1)).TotalSeconds;
            var paymentData = new Net.payOS.Types.PaymentData(
               orderCode: items.transId,
               amount: items.amount,
               description: $"Pay for {transId}",
               items: new List<Net.payOS.Types.ItemData>(),
               cancelUrl: cancelUrl,
               returnUrl: returnUrl,
               expiredAt: expiredAt
           );

            return paymentData;
        }

        private long ConvertGuidToLong(Guid guid)
        {
            Random random = new Random();

            return random.Next();
        }

        public async Task<ApiResponse<string>> PaymentUrlCallbackProcessing(Net.payOS.Types.WebhookType model)
        {
            Net.payOS.Types.WebhookData verifiedData = _payOs.verifyPaymentWebhookData(model);

            string responseCode = verifiedData.code;
            var transactionId = verifiedData.orderCode;

            var existingOrder = _unitOfWork.Orders.GetQuery()
                .Include(o => o.OrderDetails)
                .FirstOrDefault(x => x.LongOrderId == transactionId);

            if (existingOrder != null)
            {
                if (responseCode != "00")
                {
                    existingOrder.PaymentStatus = PaymentStatus.Canceled;
                    existingOrder.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Orders.Update(existingOrder);
                    return ApiResponse<string>.ErrorResponse(ResponseMessage.PaymentFailed);
                }

                existingOrder.PaymentTime = DateTime.UtcNow;
                existingOrder.PaymentStatus = PaymentStatus.Completed;
                existingOrder.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Orders.Update(existingOrder);


                //Loop through all ticket and change it status
                var ordersQuery = _unitOfWork.Orders.GetQuery();
                var trackedOrder = await ordersQuery
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Workshop)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Ticket)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Tickets)
                    .FirstOrDefaultAsync(x => x.OrderId == existingOrder.OrderId);

                foreach (var orderDetail in trackedOrder.OrderDetails)
                {
                    var amount = orderDetail.Quantity;
                    orderDetail.Workshop.Capacity -= amount;
                }

                foreach (var orderDetail in trackedOrder.OrderDetails)
                {
                    if (orderDetail.Ticket != null)
                    {
                        orderDetail.Ticket.Status = PaymentStatus.Completed;
                        orderDetail.Ticket.PaymentTime = DateTime.UtcNow;
                        orderDetail.Ticket.QrCode = _ticketService.GenerateTicketPrivateKey(orderDetail.Ticket.TicketId);
                    }

                    if (orderDetail.Tickets.Count > 0)
                    {
                        foreach (var ticket in orderDetail.Tickets)
                        {
                            ticket.Status = PaymentStatus.Completed;
                            ticket.PaymentTime = DateTime.UtcNow;
                            ticket.QrCode = _ticketService.GenerateTicketPrivateKey(ticket.TicketId);
                        }
                    }
                }

                _unitOfWork.Complete();

                return ApiResponse<string>.SuccessResponse(ResponseMessage.PaymentSuccessfully);
            }

            var transactionQuery = _unitOfWork.Transactions.GetQuery()
                .Include(ct => ct.CommissionTransactions)
                .Include(pt => pt.PromotionTransactions)
                .Include(st => st.SubscriptionTransactions)
                .Include(pm => pm.PaymentMethod)
                .Include(u => u.User)
                    .ThenInclude(o => o.Organizers)
                .Where(x => x.LongTransactionId == transactionId);

            bool exists = await transactionQuery.AnyAsync();

            if (!exists)
            {
                return ApiResponse<string>.ErrorResponse("Transaction not found.");
            }

            var transaction = await transactionQuery.FirstOrDefaultAsync();

            switch (transaction.TransactionType)
            {
                case TransactionType.Subscription:
                    {
                        await HandleSubscriptionTransactionCallBack(transaction);
                        HandlepaymentMethodCallBack(verifiedData, transaction);
                        await _unitOfWork.Transactions.Update(transaction);
                        break;
                    }
                case TransactionType.Promotion:
                    {
                        await HandlePromotionTransactionCallBack(transaction);
                        HandlepaymentMethodCallBack(verifiedData, transaction);
                        await _unitOfWork.Transactions.Update(transaction);
                        break;
                    }
                default:
                    throw new CustomException(ResponseMessage.TransactionTypeNotFound);
            }

            return ApiResponse<string>.SuccessResponse("Payment Successfully");
        }

        private async Task HandlePromotionTransactionCallBack(Transaction transaction)
        {
            if (transaction == null)
                throw new CustomException("Transaction is null.");

            if (transaction.PaymentMethod != null)
                throw new CustomException("This order have already completed!");

            Promotion promotion = new Promotion()
            {
                PromotionId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Organizer = transaction.User.Organizers.FirstOrDefault(),
            };

            PromotionTransaction existingPromotionTransaction = transaction.PromotionTransactions
                .FirstOrDefault() ??
                throw new CustomException("Promotion transaction can't be null");

            var promotionTransactionQuery = _unitOfWork.PromotionTransactions.GetQuery();

            PromotionTransaction trackingPromotion = await promotionTransactionQuery
                .Include(pt => pt.Workshop)
                .Include(pt => pt.Promotion)
                .FirstOrDefaultAsync(pt => pt.PromotionTransactionId == existingPromotionTransaction.PromotionTransactionId)
                ?? throw new CustomException($"{nameof(trackingPromotion)} can't be null");

            if (trackingPromotion.Workshop == null)
                throw new CustomException("Workshop is null due to not include.");

            promotion.Workshop = trackingPromotion.Workshop;
            promotion.StartDate = trackingPromotion.Workshop.StartTime;
            promotion.EndDate = trackingPromotion.Workshop.EndTime;
            promotion.Price = transaction.Amount;
            promotion.CurrencyCode = CurrencyCode.VietnameseCurrency;
            promotion.PromotionType = trackingPromotion.PromotionType;
            trackingPromotion.Promotion = promotion;

            await _unitOfWork.PromotionTransactions.Update(trackingPromotion);
        }

        private void HandlepaymentMethodCallBack(Net.payOS.Types.WebhookData webHookData, Transaction transaction)
        {
            PaymentMethod paymentMethod = new PaymentMethod()
            {
                PaymentMethodId = Guid.NewGuid(),
                MethodName = "VietQR",
                Description = webHookData.description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            transaction.PaymentMethod = paymentMethod;
        }

        private async Task HandleSubscriptionTransactionCallBack(Transaction transaction)
        {
            if (transaction.PaymentMethod != null)
                throw new CustomException("This order have already completed!");

            if (transaction == null)
                throw new CustomException("Transaction is null.");

            var subscription = new Subscription
            {
                SubscriptionId = Guid.NewGuid(),
                User = transaction.User,
                Tier = SubscriptionTiers.GetTier(transaction.Amount ?? throw new CustomException("Amount field of callback is null.")),
                StartDate = DateTime.UtcNow,
                EndDate = SubscriptionTiers.GetDuration(transaction.Amount ?? throw new CustomException("Amount field of callback is null.")),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            // Check if SubscriptionTransactions exist
            var existingSubscription = transaction.SubscriptionTransactions
                .FirstOrDefault()
                ?? throw new CustomException("No existing subscription found for this transaction.");

            IQueryable<SubscriptionTransaction> subscriptionTransactionQuery = _unitOfWork.SubscriptionTransactions.GetQuery();

            // Retrieve the existing tracking subscription
            var trackingSubscription = await subscriptionTransactionQuery
                .Include(st => st.Subscription)
                .FirstOrDefaultAsync(x => x.SubscriptionTransactionId == existingSubscription.SubscriptionTransactionId)
                ?? throw new CustomException("Subscription transaction not found in the database.");

            // Update the tracking subscription with new details
            trackingSubscription.Subscription = subscription;

            // Update the repository
            await _unitOfWork.SubscriptionTransactions.Update(trackingSubscription);

        }

        public Task<ApiResponse<TransactionDto>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<SubscriptionDto>>> GetSubscription(string email)
        {
            var existingUser = await _unitOfWork.Users.GetUserByUserNameAsync(email);
            if (existingUser == null)
            {
                throw new CustomException(ResponseMessage.UserNotFound);
            }

            return ApiResponse<List<SubscriptionDto>>.SuccessResponse(
                    existingUser.Subscriptions.Count > 0 ?
                    _mapper.Map<List<SubscriptionDto>>(existingUser.Subscriptions) :
                    new List<SubscriptionDto>()
                );
        }
    }
}
