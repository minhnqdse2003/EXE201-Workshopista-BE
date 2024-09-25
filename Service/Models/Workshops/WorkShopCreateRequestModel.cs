using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Workshops
{
    public class WorkShopCreateRequestModel
    {
        public Guid WorkshopId { get; set; } = Guid.NewGuid();
        public Guid OrganizerId { get; set; }
        public string Title { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryId { get; set; }
        public string? LocationCity { get; set; }
        public string? LocationDistrict { get; set; }
        public string? LocationAddress { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal? Price { get; set; }
        public string? CurrencyCode { get; set; }
        public int? Capacity { get; set; }
        public DateTime? CreatedAt { get; set; }= DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public required OrganizerRegisterRequestModel Organizer { get; set; }
        public required ICollection<WorkshopTicketRankRegisterRequestModel> TicketRanks { get; set; }
    }

    public class OrganizerRegisterRequestModel
    {
        public Guid? OrganizerId { get; set; } = Guid.NewGuid();
        public required string OrganizationName { get; set; }
        public required string ContactEmail { get; set; }
        public required string ContactPhone { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? SocialLinks { get; set; }
        public bool? Verified { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class WorkshopTicketRankRegisterRequestModel
    {
        public Guid? TicketRankId { get; set; } = Guid.NewGuid();

        public Guid? WorkshopId { get; set; }

        public string RankName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
