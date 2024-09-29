using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Workshops
{
    public class WorkshopFilterModel
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
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Price { get; set; }
        public string? CurrencyCode { get; set; }
        public int? Capacity { get; set; }
        public int? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
