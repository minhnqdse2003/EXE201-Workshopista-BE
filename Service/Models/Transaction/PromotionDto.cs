using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Transaction
{
    public class PromotionDto
    {
        public Guid PromotionId { get; set; }

        public Guid? OrganizerId { get; set; }

        public Guid? WorkshopId { get; set; }

        public string? PromotionType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? Price { get; set; }

        public string? CurrencyCode { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
