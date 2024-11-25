using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Workshops
{
    public class WorkShopResponseModel
    {
        public Guid WorkshopId { get; set; }
        public string? Title { get; set; }

        public string? Slug { get; set; }

        public string? Description { get; set; }

        public Guid? CategoryId { get; set; }

        public string? LocationCity { get; set; }

        public string? LocationDistrict { get; set; }

        public string? LocationAddress { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public decimal? Price { get; set; }

        public string? CurrencyCode { get; set; }

        public int? Capacity { get; set; }

        public string? VideoUrl { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual OrganizerResponseModel? Organizer { get; set; }

        public virtual ICollection<WorkshopImageResponseModel> WorkshopImages { get; set; } = new List<WorkshopImageResponseModel>();

        public virtual ICollection<TicketRankModelResponse>? TicketRanks { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

    public class WorkShopResponseModelWithPagination : PaginationModel
    {
        public List<WorkShopResponseModel> workShops { get; set; }
    }

    public class PaginationModel
    {
        public int? CurrentPage { get; set; }
        public int? Total { get; set; }
    }

    public class OrganizerResponseModel
    {
        public Guid OrganizerId { get; set; }

        public Guid? UserId { get; set; }

        public string? OrganizationName { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactPhone { get; set; }

        public string? WebsiteUrl { get; set; }

        public string? SocialLinks { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual UserResponseModel? User { get; set; }
    }

    public class WorkshopImageResponseModel
    {
        public Guid ImageId { get; set; }

        public Guid? WorkshopId { get; set; }

        public string? ImageUrl { get; set; }

        public bool? IsPrimary { get; set; }

        public DateTime? CreatedAt { get; set; }
    }

    public class UserResponseModel
    {
        public Guid UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
    }

    public class TicketRankModelResponse
    {
        public Guid TicketRankId { get; set; }

        public string RankName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }
    }

    public class ReviewsModelResponse
    {
        public Guid ReviewId { get; set; }

        public Guid? WorkshopId { get; set; }

        public Guid? ParticipantId { get; set; }

        public short? Rating { get; set; }

        public string? Comment { get; set; }

        public string? ReviewStatus { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
