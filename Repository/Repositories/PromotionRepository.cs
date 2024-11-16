using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public int CountNumberOfPromotionType(Guid workshopId)
        {
            return _context.Promotions
                .Where(p => p.WorkshopId == workshopId)
                .Count();
        }

        public int CountOverlappingPromotions(string promotionType, DateTime workshopStartDate, DateTime workshopEndDate)
        {
            return _context.Promotions
                .Where(p => p.PromotionType == promotionType &&
                            p.StartDate < workshopEndDate &&
                            p.EndDate > workshopStartDate)
                .Count();

        }

        public IQueryable<Promotion> GetQuery()
        {
            return _context.Promotions.AsQueryable();
        }
    }
}
