using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Workshops
{
    public class WorkShopUpdateRequestModel
    { 
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryId { get; set; }
        public string? LocationCity { get; set; }
        public string? LocationDistrict { get; set; }
        public string? LocationAddress { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }
        public string? VideoUrl { get; set; }
        public string? Status { get; set; }
        public OrganizerUpdateRequestModel? Organizer { get; set; }
        public List<TicketRankUpdateModel>? TicketRanks { get; set; }
        public List<IFormFile>? WorkshopImages { get; set; }
        public List<string>? RemainingWorkshop { get; set; }
    }
    public class TicketRankUpdateModel
    {

        public string RankName { get; set; } = null!;

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }
    }
    public class OrganizerUpdateRequestModel
    {
        public string? OrganizationName { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? SocialLinks { get; set; }
        public bool? Verified { get; set; }
    }
}
