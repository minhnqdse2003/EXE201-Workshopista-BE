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
    public class TicketRankRepository : GenericRepository<TicketRank>, ITicketRankRepository
    {
        public TicketRankRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public async Task<List<TicketRank>> GetAllTicketRankOfWorkshop(Guid workshopId)
        {
            return await _context.TicketRanks.Where(t => t.WorkshopId == workshopId).ToListAsync();
        }

        public async Task<TicketRank?> GetTicketRankById(Guid id)
        {
            return await _context.TicketRanks.Include(t => t.Workshop).FirstOrDefaultAsync(t => t.TicketRankId == id);
        }
    }
}
