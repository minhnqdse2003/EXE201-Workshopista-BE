using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Repository.Consts;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.Models;
using Service.Models.Ticket;
using System;
using System.Reflection.PortableExecutable;

namespace Service.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public string GenerateQRCode(Guid ticketId)
        {
            var existingTicket = _unitOfWork.Tickets.GetById(ticketId);
            if (existingTicket == null)
            {
                throw new CustomException(ResponseMessage.TicketNotFound);
            }

            // Generate the hash for the ticket ID
            string ticketHash = GenerateTicketPrivateKey(ticketId);

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(ticketHash, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                string base64String = Convert.ToBase64String(qrCodeImage);

                return base64String;
            }
        }

        public async Task<ApiResponse<List<ListTicketDto>>> GetUserTicket(string userName)
        {
            var existingUser = await _unitOfWork.Users.GetUserByEmail(userName);
            if (existingUser == null)
            {
                throw new CustomException(ResponseMessage.UserNotFound);
            }

            var orderDetails = _unitOfWork.OrderDetails.GetQuery()
                .Where(od => od.Order.ParticipantId == existingUser.UserId)
                .Select(od => new
                {
                    WorkshopId = od.Workshop.WorkshopId,
                    WorkshopTitle = od.Workshop.Title,
                    Tickets = od.Tickets
                        .Where(t => t.Status != null)
                        .Select(t => new ListTicketDto
                        {
                            TicketId = t.TicketId,
                            WorkshopId = od.Workshop.WorkshopId,
                            WorkshopTitle = od.Workshop.Title,
                            Price = t.Price,
                            CurrencyCode = t.CurrencyCode,
                            Status = t.Status,
                            TicketRank = t.TicketRank
                        }).ToList(),
                    SingleTicket = od.Ticket != null && od.Ticket.Status != null
                        ? new ListTicketDto
                        {
                            TicketId = od.Ticket.TicketId,
                            WorkshopId = od.Workshop.WorkshopId,
                            WorkshopTitle = od.Workshop.Title,
                            Price = od.Ticket.Price,
                            CurrencyCode = od.Ticket.CurrencyCode,
                            Status = od.Ticket.Status,
                            TicketRank = od.Ticket.TicketRank
                        }
                        : null
                })
                .AsEnumerable()
                .SelectMany(od => od.Tickets.Any() ? od.Tickets : od.SingleTicket != null ? new List<ListTicketDto> { od.SingleTicket } : new List<ListTicketDto>())
                .OrderByDescending(ticket => ticket.TicketId)
                .ToList();

            if (!orderDetails.Any())
            {
                return ApiResponse<List<ListTicketDto>>.SuccessResponse(new List<ListTicketDto>(), ResponseMessage.ReadSuccess);
            }

            return ApiResponse<List<ListTicketDto>>.SuccessResponse(orderDetails, ResponseMessage.ReadSuccess);

        }

        public async Task<bool> Verify(string hashData)
        {
            var ticketList = _unitOfWork.Tickets.GetAll();

            Ticket existingTicket = new Ticket();

            foreach (var ticket in ticketList)
            {
                var verifyResult = VerifyQRCode(hashData, ticket.TicketId);
                if (verifyResult)
                {
                    existingTicket = ticket;
                    break;
                }
            }
            if (existingTicket.Status != PaymentStatus.Completed)
            {
                throw new CustomException(ResponseMessage.TicketNotFound);
            }

            // Update the ticket status to confirmed
            existingTicket.Status = TicketStatus.Confirmed;
            await _unitOfWork.Tickets.Update(existingTicket);

            return true;
        }


        public bool VerifyQRCode(string hashData, Guid ticketId)
        {
            return BCrypt.Net.BCrypt.Verify(ticketId.ToString(), hashData);
        }

        public string GenerateTicketPrivateKey(Guid ticketId)
        {
            string hashedTicketId = BCrypt.Net.BCrypt.HashPassword(ticketId.ToString());
            return hashedTicketId;
        }

        public async Task<ApiResponse<Ticket>> UpdateTicket(TicketUpdateModel updateModel)
        {
            var existingTicket = _unitOfWork.Tickets.GetById(updateModel.TicketId);
            if (existingTicket == null)
            {
                throw new CustomException(ResponseMessage.TicketNotFound);
            }

            _mapper.Map(updateModel, existingTicket);
            await _unitOfWork.Tickets.Update(existingTicket);

            return ApiResponse<Ticket>.SuccessResponse(existingTicket);
        }
    }
}
