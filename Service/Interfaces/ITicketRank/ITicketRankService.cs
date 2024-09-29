using Service.Models.TicketRank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.ITicketRank
{
    public interface ITicketRankService
    {
        Task<List<TicketRankResponseModel>> GetAllTicketRankOfWorkshop(Guid workshopId);

        Task<TicketRankResponseModel> GetTicketRankById(Guid id);

        Task CreateTicketRank(TicketRankRequestModel model, Guid workshopId);

        Task UpdateTicketRank(TicketRankRequestModel model, Guid id);

        Task DeleteTicketRank(Guid id);
    }
}
