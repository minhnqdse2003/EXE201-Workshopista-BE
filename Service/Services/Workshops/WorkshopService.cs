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

namespace Service.Services.Workshops
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
                throw new Exception(ResponseMessage.UserNotFound + " " + ResponseMessage.FromTokenClaims);
            }

            //Find organizer and attach to workshop
            var organizer = _mapper.Map<Organizer>(workshopCreateDto.Organizer);
            organizer.User = existingUser;

            //Find category
            var category = _unitOfWork.Categories.GetById((Guid)workshopCreateDto.CategoryId);
            if (category == null)
            {
                throw new Exception(ResponseMessage.CategoryNotFound + "" + ResponseMessage.FromRequestModel);
            }

            var ticketRank = _mapper.Map<List<TicketRank>>(workshopCreateDto.TicketRanks);
            var ticketRankCount = ticketRank.Count();

            if (ticketRankCount <= 0)
            {
                throw new Exception(ResponseMessage.TicketRankEmpty + ResponseMessage.FromRequestModel);
            }

            var workshop = _mapper.Map<Workshop>(workshopCreateDto);
            workshop.Category = category;
            workshop.Organizer = organizer;
            workshop.TicketRanks = ticketRank;
            workshop.Capacity = ticketRank.Sum(x => x.Capacity);

            _unitOfWork.Workshops.Add(workshop);
            _unitOfWork.Complete();

            var workshopDto = _mapper.Map<WorkShopResponseModel>(workshop);
            return ApiResponse<WorkShopResponseModel>.SuccessResponse(workshopDto, ResponseMessage.CreateSuccess);
        }

    }
}
