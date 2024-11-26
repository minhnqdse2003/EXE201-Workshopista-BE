using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Repository.Consts;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(Exe201WorkshopistaContext context) : base(context)
        {
        }

        public IQueryable<Ticket> GetQuery() => _context.Tickets.AsQueryable();

        public async Task<Ticket?> GetTicketAsyncByQrCode(string hashQrContent)
        {
            return await _context.Tickets.FirstOrDefaultAsync(x => x.QrCode == hashQrContent);
        }

        public async Task<List<Ticket>> GetBoughtTicketsByWorkshopId(Guid workshopId)
        {
            var list = await _context.Tickets.Where(t => t.WorkshopId == workshopId && t.Status.Equals(TicketStatus.Confirmed)).ToListAsync();
            return list;
        }
    }
}
