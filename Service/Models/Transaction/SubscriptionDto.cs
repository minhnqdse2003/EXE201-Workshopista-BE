using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Transaction
{
    public class SubscriptionDto
    {
        public Guid SubscriptionId { get; set; }

        public Guid? UserId { get; set; }

        public string? Tier { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? AutoRenew { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
