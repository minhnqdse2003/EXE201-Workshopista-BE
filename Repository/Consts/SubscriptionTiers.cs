using Microsoft.CodeAnalysis.CSharp;
using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Consts
{
    public static class SubscriptionTiers
    {
        // Tier Names
        public const string FreeTier = "Free";
        public const string PremiumBasic = "Premium Basic";
        public const string PremiumPro = "Premium Pro";
        public const string PremiumAnnual = "Premium Annual";

        // Tier Minimum Prices (for participants)
        public static class ParticipantPrices
        {
            public const decimal PremiumBasicMinPrice = 50000.00m; // VND
            public const decimal PremiumProMinPrice = 150000.00m; // VND
            public const decimal PremiumAnnualMinPrice = 1200000.00m; // VND
        }
        public static decimal GetParticipantPrice(string tier)
        {
            return tier switch
            {
                PremiumBasic => ParticipantPrices.PremiumBasicMinPrice,
                PremiumPro => ParticipantPrices.PremiumProMinPrice,
                PremiumAnnual => ParticipantPrices.PremiumAnnualMinPrice,
                _ => throw new CustomException("Invalid tier")
            };
        }

        public static string GetTier(decimal amount)
        {
            if (amount < 0)
                throw new CustomException("Amount must be non-negative");

            return amount switch
            {
                < ParticipantPrices.PremiumBasicMinPrice => FreeTier,
                < ParticipantPrices.PremiumProMinPrice => PremiumBasic,
                <= ParticipantPrices.PremiumAnnualMinPrice => PremiumPro,
                _ => PremiumAnnual
            };
        }

        public static DateTime? GetDuration(decimal amount)
        {
            string tier = GetTier(amount); // Assuming GetTier method is already defined

            return tier switch
            {
                FreeTier => DateTime.UtcNow, // For free tier, you might want to set it to MaxValue or handle it differently
                PremiumBasic => DateTime.UtcNow.AddMonths(1),
                PremiumPro => DateTime.UtcNow.AddMonths(1),
                PremiumAnnual => DateTime.UtcNow.AddYears(1),
                _ => throw new CustomException("Invalid tier")
            };
        }
    }
}
