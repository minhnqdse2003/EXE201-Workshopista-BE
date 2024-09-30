using Microsoft.AspNetCore.Http;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service.Models.Workshops
{
    public class WorkShopCreateRequestModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public string LocationCity { get; set; }
        public string LocationDistrict { get; set; }
        public string LocationAddress { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public int Capacity { get; set; }
        public required OrganizerRegisterRequestModel Organizer { get; set; }
        public required ICollection<WorkshopTicketRankRegisterRequestModel> TicketRanks { get; set; }
        public required List<IFormFile> WorkshopImages { get; set; }
    }

    public class OrganizerRegisterRequestModel
    {
        public required string OrganizationName { get; set; }
        public required string ContactEmail { get; set; }
        public required string ContactPhone { get; set; }
        public string WebsiteUrl { get; set; }
        public string SocialLinks { get; set; }
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
