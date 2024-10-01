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

namespace Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public TransactionService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
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
                        items.Add("transId", order.OrderId.ToString());
                        items.Add("amount", order.TotalAmount.ToString());
                        break;
                    }
                case TransactionType.Subscription:
                    {
                        Transaction transaction = await CreateTransaction(existingUser, requestModel);
                        var totalAmount = await HandleSubscriptionTransaction(transaction, requestModel.Subscriptions);
                        items.Add("transId", transaction.TransactionId.ToString());
                        items.Add("amount", totalAmount.ToString());
                        break;
                    }
                case TransactionType.Promotion:
                    {
                        Transaction transaction = await CreateTransaction(existingUser, requestModel);
                        var totalAmount =  await HandlePromotionTransaction(transaction, requestModel.Promotions);
                        items.Add("transId", transaction.TransactionId.ToString());
                        items.Add("amount", totalAmount.ToString());
                        break;
                    }
                case TransactionType.Commission:
                    break;
                default:
                    throw new CustomException(ResponseMessage.TransactionTypeNotFound);
            }

            var param = GenerateZaloPayParameters(items);
            var result = HttpHelper.PostFormAsync(_configuration["ZaloPayment:createOrderUrl"], param).Result;

            return ApiResponse<Object>.SuccessResponse(result, ResponseMessage.PaymentLinkCreateSuccess);
        }

        private async Task<decimal> HandlePromotionTransaction(Transaction transaction, PromotionRequestModel? promotions)
        {
            if (promotions == null)
                throw new CustomException(ResponseMessage.InvalidInput + "[promotion model]");
            var existingWorkshop = await GetWorkshopWithTicketRanks(promotions.WorkshopId ?? Guid.NewGuid());

            //Create Promotion Transaction
            var promotionTransaction = new PromotionTransaction()
            {
                PromotionTransactionId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Workshop = existingWorkshop,
            };
            //Assign promotion amount

            transaction.Amount = 100000.00m;
            transaction.PromotionTransactions.Add(promotionTransaction);

            await _unitOfWork.Transactions.Update(transaction);

            return transaction.Amount ?? 100000.00m;
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

                var existingWorkshop = await GetWorkshopWithTicketRanks(orderDetails.WorkshopId);
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

            await _unitOfWork.Orders.Add(order);

            var existingTicket = _unitOfWork.OrderDetails.GetQuery();

            var ticket = await existingTicket
                .Include(od => od.Ticket)
                .FirstOrDefaultAsync(x => x.OrderDetailsId == updateOrderDetailModels.FirstOrDefault().OrderDetailsId);

            return order;
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

        private async Task<Workshop> GetWorkshopWithTicketRanks(Guid workshopId)
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

        private Dictionary<string, string> GenerateZaloPayParameters(Dictionary<string, string> itemsModel)
        {
            string transId = itemsModel["transId"];

            string amount = itemsModel["amount"];
            string formattedAmount = amount.Substring(0, amount.Length - 3);

            var items = new
            {
                transId = transId,
                amount = amount,
            };
            string appid = _configuration["ZaloPayment:appid"] 
                ?? throw new CustomException($"{nameof(appid)} not found.{nameof(GenerateZaloPayParameters)}");
            string key1 = _configuration["ZaloPayment:key1"]
                ?? throw new CustomException($"{nameof(key1)} not found.{nameof(GenerateZaloPayParameters)}");
            string createOrderUrl = _configuration["ServerName"] + _configuration["ZaloPayment:redirectUrl"]
                ?? throw new CustomException($"{nameof(createOrderUrl)} not found.{nameof(GenerateZaloPayParameters)}");

            var embeddata = new { merchantinfo = "embeddata123", redirecturl = createOrderUrl };


            var param = new Dictionary<string, string>
            {
                { "appid", appid },
                { "appuser", "demo" },
                { "apptime", Utils.GetTimeStamp().ToString() },
                { "amount", formattedAmount},
                { "apptransid", DateTime.Now.ToString("yyMMdd") + "_" + transId },
                { "embeddata", JsonConvert.SerializeObject(embeddata) },
                { "item", JsonConvert.SerializeObject(items) },
                { "description", "ZaloPay demo" },
                { "bankcode", "zalopayapp" }
            };

            var data = $"{appid}|{param["apptransid"]}|{param["appuser"]}|{param["amount"]}|{param["apptime"]}|{param["embeddata"]}|{param["item"]}";
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

            return param;
        }

        public async Task<ApiResponse<string>> PaymentUrlCallbackProcessing(ZaloPayCallbackModel model)
        {
            var transactionId = Guid.Parse(model.AppTransId.ToString().Split('_').LastOrDefault());

            var existingOrder = _unitOfWork.Orders.GetById(transactionId);

            if (existingOrder != null)
            {
                existingOrder.PaymentTime = DateTime.UtcNow;
                existingOrder.PaymentStatus = PaymentStatus.Completed;
                existingOrder.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Orders.Update(existingOrder);


                //Loop through all ticket and change it status
                var ordersQuery = _unitOfWork.Orders.GetQuery();
                var trackedOrder = await ordersQuery
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Ticket)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Tickets)
                    .FirstOrDefaultAsync(x => x.OrderId == existingOrder.OrderId);

                foreach (var orderDetail in trackedOrder.OrderDetails)
                {
                    if (orderDetail.Ticket != null)
                    {
                        orderDetail.Ticket.Status = PaymentStatus.Completed;  // Update ticket status
                        orderDetail.Ticket.PaymentTime = DateTime.UtcNow;  // Update timestamp (if you have a field for this)
                    }

                    if(orderDetail.Tickets.Count > 0)
                    {
                        foreach (var ticket in orderDetail.Tickets)
                        {
                            ticket.Status = PaymentStatus.Completed;
                            ticket.PaymentTime = DateTime.UtcNow;
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
                .Where(x => x.TransactionId == transactionId);

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
                        HandlepaymentMethodCallBack(transaction);
                        await _unitOfWork.Transactions.Update(transaction);
                        break;
                    }
                default:
                    throw new CustomException(ResponseMessage.TransactionTypeNotFound);
            }

            return ApiResponse<string>.SuccessResponse("Payment Successfully");
        }

        private void HandlepaymentMethodCallBack(Transaction transaction)
        {
            PaymentMethod paymentMethod = new PaymentMethod()
            {
                PaymentMethodId = Guid.NewGuid(),
                MethodName = "Zalopay",
                Description = "Empty",
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

    }
}
