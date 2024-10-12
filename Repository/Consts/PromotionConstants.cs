using Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Consts
{
    public static class PromotionConstants
    {
        public static class PromotionBasePrice
        {
            public const decimal BannerBasePrice = 100000.00m;
            public const decimal FeaturedBasePrice = 200000.00m;
            public const decimal HighlightBasePrice = 300000.00m;
            public const decimal DemandFactorIncrease = 0.10m; // 10% increase per overlapping promotion
        }

        public static decimal GetPromotionPrice(string type)
        {
            return type switch
            {
                Banner => PromotionBasePrice.BannerBasePrice,
                Featured => PromotionBasePrice.FeaturedBasePrice,
                Highlight => PromotionBasePrice.HighlightBasePrice,
                _ => throw new CustomException($"Invalid type for promotion({Banner},{Featured},{Highlight})")
            };
        }

        public static string GetType(string type)
        {
            return type switch
            {
                Banner => Banner,
                Featured => Featured,
                Highlight => Highlight,
                _ => throw new CustomException($"Invalid type for promotion ({Banner},{Featured},{Highlight})")
            };
        }

        public static class PromotionQuantityMaxLength
        {
            public const int BannerLenght = 100;
            public const int FeaturedLenght = 100;
            public const int HighlightLenght = 100;
        }

        public const string Banner = "Banner";
        public const string Featured = "Featured";
        public const string Highlight = "Highlight";
    }
}
