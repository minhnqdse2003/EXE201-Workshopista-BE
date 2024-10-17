using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public async Task<List<Review>> GetReviewByWorkshopId(Guid workshopId)
        {
            return await _context.Reviews.Where(r => r.WorkshopId.Equals(workshopId)).Include(r => r.Participant).Include(r => r.Workshop).ToListAsync();
        }
    }
}
