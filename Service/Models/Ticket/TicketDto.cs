using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Ticket
{
    public class TicketDto
    {
        public Guid TicketId { get; set; }
        public Guid WorkshopId { get; set; }
        public string? WorkshopTitle { get; set; }
        public decimal? Price { get; set; }
        public string? CurrencyCode { get; set; }
        public string? Status { get; set; }
        public string? QrCode { get; set; }
        public virtual Repository.Models.TicketRank? TicketRank { get; set; }
    }

    public class ListTicketDto
    {
        public Guid TicketId { get; set; }
        public Guid WorkshopId { get; set; }
        public string? WorkshopTitle { get; set; }
        public decimal? Price { get; set; }
        public string? CurrencyCode { get; set; }
        public string? Status { get; set; }
        public virtual Repository.Models.TicketRank? TicketRank { get; set; }
    }
}
