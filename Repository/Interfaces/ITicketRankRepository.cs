using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITicketRankRepository : IGenericRepository<TicketRank>
    {
        Task<TicketRank?> GetTicketRankById(Guid id);
        Task<List<TicketRank>> GetAllTicketRankOfWorkshop(Guid workshopId);
    }
}
