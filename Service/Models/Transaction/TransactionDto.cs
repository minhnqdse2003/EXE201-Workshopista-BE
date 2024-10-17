using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Transaction
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid? UserId { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public string? CurrencyCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual ICollection<CommissionTransaction> CommissionTransactions { get; set; } = new List<CommissionTransaction>();
        public virtual ICollection<PromotionTransaction> PromotionTransactions { get; set; } = new List<PromotionTransaction>();
        public virtual ICollection<SubscriptionTransaction> SubscriptionTransactions { get; set; } = new List<SubscriptionTransaction>();
    }
}
