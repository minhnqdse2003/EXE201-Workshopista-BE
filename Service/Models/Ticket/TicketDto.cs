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

    public class TicketDetailsDto
    {
        public required TicketDetailsWorkshopDto WorkshopDetails { get; set; }
        public required decimal Price { get; set; }
        public required string Status { get; set; }
        public required string RankName { get; set; }
        public required string QrCode { get; set; }
    }

    public class TicketDetailsWorkshopDto
    {
        public required string Title { get; set; }
        public required DateTime StartTime { get; set; }
        public required string LocationCity { get; set; }
        public required string LocationDistrict { get; set; }
        public required string LocationAddress { get; set; }
        public required string WorkshopImage { get; set; }
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
