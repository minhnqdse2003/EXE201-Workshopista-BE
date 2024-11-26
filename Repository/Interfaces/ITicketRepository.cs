using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<Ticket?> GetTicketAsyncByQrCode(string hashQrContent);
        IQueryable<Ticket> GetQuery();
        Task<List<Ticket>> GetBoughtTicketsByWorkshopId(Guid workshopId);
    }
}
