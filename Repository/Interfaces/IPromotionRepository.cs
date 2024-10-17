using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPromotionRepository : IGenericRepository<Promotion>
    {
        int CountNumberOfPromotionType(Guid workshopId);
        int CountOverlappingPromotions(string promotionType, DateTime workshopStartDate, DateTime workshopEndDate);
    }
}
