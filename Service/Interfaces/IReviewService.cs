using Repository.Models;
using Service.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IReviewService
    {
        Task CreateReview(ReviewCreateModel model, string token);
        Task<List<Review>> GetAllReviewOfWorkshop(Guid workshopId);
    }
}
