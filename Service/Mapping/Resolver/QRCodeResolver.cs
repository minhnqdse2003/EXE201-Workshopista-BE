using AutoMapper;
using Repository.Models;
using Service.Interfaces;
using Service.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping.Resolver
{
    public class QRCodeResolver : IValueResolver<Ticket, TicketDto, string>
    {
        private readonly ITicketService _ticketService;

        public QRCodeResolver(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public string Resolve(Ticket source, TicketDto destination, string destMember, ResolutionContext context)
        {
            // Use the injected ticket service to generate the QR code
            return _ticketService.GenerateQRCode(source.TicketId);
        }
    }

}
