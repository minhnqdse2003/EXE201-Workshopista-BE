using AutoMapper;
using Repository.Helpers;
using Repository.Interfaces;
using Service.Interfaces;
using Service.Models;
using Repository.Models;
using Service.Models.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Repository.Consts;

namespace Service.Services
{
    public class WorkshopService : IWorkshopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkshopService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<WorkShopResponseModel>>> GetFilter(WorkshopFilterModel filterModel)
        {
            var query = _unitOfWork.Workshops.Get();

            if (!string.IsNullOrEmpty(filterModel.Title))
                query = query.Where(w => w.Title.Contains(filterModel.Title));
            if (filterModel.CategoryId.HasValue)
                query = query.Where(w => w.CategoryId == filterModel.CategoryId);
            if (!string.IsNullOrEmpty(filterModel.LocationCity))
                query = query.Where(w => w.LocationCity == filterModel.LocationCity);
            if (filterModel.Price.HasValue)
                query = query.Where(w => w.Price == filterModel.Price);
            if (filterModel.StartTime.HasValue)
                query = query.Where(w => w.StartTime >= filterModel.StartTime);
            if (filterModel.EndTime.HasValue)
                query = query.Where(w => w.EndTime <= filterModel.EndTime);
            if (filterModel.Status.HasValue)
            {
                string currStatus = filterModel.Status > 0 ? StatusConst.Active : StatusConst.InActive;
                query = query.Where(w => w.Status == currStatus);
            }

            var workshops = await query
                .Include(x => x.Organizer)
                    .ThenInclude(x => x.User)
                .Include(x => x.TicketRanks)
                .Include(x => x.WorkshopImages)
                .Skip((filterModel.Page - 1) * filterModel.PageSize)
                .Take(filterModel.PageSize)
                .ToListAsync();

            var workshopDtos = _mapper.Map<IEnumerable<WorkShopResponseModel>>(workshops);

            return ApiResponse<IEnumerable<WorkShopResponseModel>>.SuccessResponse(workshopDtos, ResponseMessage.ReadSuccess);
        }

        public ApiResponse<WorkShopResponseModel> GetWorkshopById(Guid id)
        {
            var workshop = _unitOfWork.Workshops.GetById(id);
            if (workshop == null)
            {
                return ApiResponse<WorkShopResponseModel>.ErrorResponse(ResponseMessage.ItemNotFound);
            }

            var workshopDto = _mapper.Map<WorkShopResponseModel>(workshop);
            return ApiResponse<WorkShopResponseModel>.SuccessResponse(workshopDto, ResponseMessage.ReadSuccess);

        }

        public async Task<ApiResponse<WorkShopResponseModel>> AddWorkshop(WorkShopCreateRequestModel workshopCreateDto, string email)
        {
            var existingUser = await _unitOfWork.Users.GetUserByUserNameAsync(email);
            if (existingUser == null)
            {
                throw new CustomException(ResponseMessage.UserNotFound + " " + ResponseMessage.FromTokenClaims);
            }

            //Find organizer and attach to workshop
            var organizer = _mapper.Map<Organizer>(workshopCreateDto.Organizer);
            organizer.User = existingUser;

            //Find category
            var category = _unitOfWork.Categories.GetById((Guid)workshopCreateDto.CategoryId);
            if (category == null)
            {
                throw new CustomException(ResponseMessage.CategoryNotFound + "" + ResponseMessage.FromRequestModel);
            }

            var ticketRank = _mapper.Map<List<TicketRank>>(workshopCreateDto.TicketRanks);
            var ticketRankCount = ticketRank.Count();

            if (ticketRankCount <= 0)
            {
                throw new CustomException(ResponseMessage.TicketRankEmpty + ResponseMessage.FromRequestModel);
            }

            var workshop = _mapper.Map<Workshop>(workshopCreateDto);
            workshop.Category = category;
            workshop.Organizer = organizer;
            workshop.TicketRanks = ticketRank;
            workshop.Status = StatusConst.InActive;
            workshop.Capacity = ticketRank.Sum(x => x.Capacity);

            await _unitOfWork.Workshops.Add(workshop);
            _unitOfWork.Complete();

            var workshopDto = _mapper.Map<WorkShopResponseModel>(workshop);
            return ApiResponse<WorkShopResponseModel>.SuccessResponse(workshopDto, ResponseMessage.CreateSuccess);
        }

        public ApiResponse<bool> DeleteWorkshop(string id)
        {
            Guid workShopId = Guid.Parse(id);
            var existingWorkshop = _unitOfWork.Workshops.GetById(workShopId);
            if (existingWorkshop == null)
            {
                throw new CustomException(ResponseMessage.WorkshopNotFound + ResponseMessage.FromRequestModel);
            }
            existingWorkshop.Status = StatusConst.InActive;
            _unitOfWork.Workshops.Update(existingWorkshop);
            _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResponse(true,ResponseMessage.DeleteSuccess);
        }

        public ApiResponse<WorkShopResponseModel> UpdateWorkshop(WorkShopUpdateRequestModel workshopUpdateDto,string id)
        {
            // Fetch the workshop by id
            var query = _unitOfWork.Workshops.Get();
            var existingWorkshop = query
                                .Include(x => x.Category)
                                .Include(x => x.TicketRanks)
                                .Include(x => x.Organizer)
                                .FirstOrDefault(x => x.WorkshopId == Guid.Parse(id));
            if (existingWorkshop == null)
            {
                throw new CustomException(ResponseMessage.WorkshopNotFound + ResponseMessage.FromRequestModel);
            }

            // Map the updated properties from the DTO to the existing workshop
            _mapper.Map(workshopUpdateDto, existingWorkshop);

            // Update related entities like Category and TicketRanks
            if (workshopUpdateDto.CategoryId.HasValue)
            {
                var category = _unitOfWork.Categories.GetById(workshopUpdateDto.CategoryId.Value);
                if (category == null)
                {
                    throw new CustomException(ResponseMessage.CategoryNotFound + ResponseMessage.FromRequestModel);
                }
                existingWorkshop.Category = category;
            }

            if (workshopUpdateDto.TicketRanks != null && workshopUpdateDto.TicketRanks.Any())
            {
                // Update ticket ranks logic
                var updatedTicketRanks = _mapper.Map<List<TicketRank>>(workshopUpdateDto.TicketRanks);
                existingWorkshop.TicketRanks.Clear();
                existingWorkshop.TicketRanks = updatedTicketRanks;
                existingWorkshop.Capacity = updatedTicketRanks.Sum(x => x.Capacity);
            }

            // Update the workshop in the database
            _unitOfWork.Workshops.Update(existingWorkshop);
            _unitOfWork.Complete();

            // Map to response model and return
            var updatedWorkshopDto = _mapper.Map<WorkShopResponseModel>(existingWorkshop);
            return ApiResponse<WorkShopResponseModel>.SuccessResponse(updatedWorkshopDto, ResponseMessage.UpdateSuccess);
        }

    }
}
