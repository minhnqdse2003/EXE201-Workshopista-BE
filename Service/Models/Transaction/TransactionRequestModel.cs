using Repository.Consts;
using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Transaction
{
    public class TransactionRequestModel
    {
        public Guid? ParticipantId { get; set; }
        public List<OrderRequestModel>? Orders { get; set; } = null;
        public CommissionRequestModel? Commissions { get; set; }
        public SubscriptionRequestModel? Subscriptions { get; set; }
        public PromotionRequestModel? Promotions { get; set; }
        public required string TransactionType { get; set; }
    }

    public class CommissionRequestModel
    {
    }


    public class OrderRequestModel
    {
        public Guid WorkshopId { get; set; }
        public int? Quantity { get; set; }
        public Guid? TicketRankId { get; set; }
    }

    public class PromotionRequestModel
    {
        public Guid? WorkshopId { get; set; }
        public string? PromotionType { get; set; }
    }

    public class SubscriptionRequestModel
    {
        public string? Tier { get; set; }

        private static readonly HashSet<string> ValidTiers = new HashSet<string>
        {
            SubscriptionTiers.FreeTier,
            SubscriptionTiers.PremiumBasic,
            SubscriptionTiers.PremiumPro,
            SubscriptionTiers.PremiumAnnual,
        };

        public void Validate()
        {
            // Validate Tier
            if (!IsValidTier(Tier))
            {
                throw new CustomException("Invalid tier. Please select a valid tier.");
            }
        }

        private bool IsValidTier(string? tier)
        {
            return !string.IsNullOrEmpty(tier) && ValidTiers.Contains(tier);
        }
    }


}
