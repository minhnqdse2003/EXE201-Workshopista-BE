using AutoMapper;
using Repository.Consts;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces.ITicketRank;
using Service.Models.TicketRank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TicketRanks
{
    public class TicketRankService : ITicketRankService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketRankService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TicketRankResponseModel>> GetAllTicketRankOfWorkshop(Guid workshopId)
        {
            var result = await _unitOfWork.TicketRanks.GetAllTicketRankOfWorkshop(workshopId);
            return result.Select(t => new TicketRankResponseModel
            {
                TicketRankId = t.TicketRankId,
                WorkshopId = t.WorkshopId,
                RankName = t.RankName,
                Description = t.Description,
                Price = t.Price,
                Capacity = t.Capacity,
            }).ToList();
        }

        public async Task<TicketRankResponseModel> GetTicketRankById(Guid id)
        {
            var result = await _unitOfWork.TicketRanks.GetTicketRankById(id);
            return new TicketRankResponseModel
            {
                TicketRankId = result.TicketRankId,
                WorkshopId = result.WorkshopId,
                RankName = result.RankName,
                Description = result.Description,
                Price = result.Price,
                Capacity = result.Capacity
            };
        }

        public async Task CreateTicketRank(TicketRankRequestModel model, Guid workshopId)
        {
            TicketRank newTicketRank = _mapper.Map<TicketRank>(model);
            newTicketRank.TicketRankId = Guid.NewGuid();
            newTicketRank.WorkshopId = workshopId;
            newTicketRank.CreatedAt = DateTime.Now;
            await _unitOfWork.TicketRanks.Add(newTicketRank);
        }

        public async Task UpdateTicketRank(TicketRankRequestModel model, Guid id)
        {
            var ticketRank = await _unitOfWork.TicketRanks.GetTicketRankById(id);
            if (ticketRank == null)
            {
                throw new CustomException("The ticket rank is not existed!");
            }

            if (ticketRank.Workshop.Status.Equals(StatusConst.Approved))
            {
                throw new CustomException("The workshop is approved. Cannot update the ticket rank!");
            }
            _mapper.Map(model, ticketRank);
            await _unitOfWork.TicketRanks.Update(ticketRank);
        }

        public async Task DeleteTicketRank(Guid id)
        {
            var ticketRank = await _unitOfWork.TicketRanks.GetTicketRankById(id);
            if (ticketRank == null)
            {
                throw new CustomException("The ticket rank is not existed!");
            }

            if (ticketRank.Workshop.Status.Equals(StatusConst.Approved))
            {
                throw new CustomException("The workshop is approved. Cannot delete the ticket rank!");
            }
            _unitOfWork.TicketRanks.Remove(ticketRank);
        }


    }
}
