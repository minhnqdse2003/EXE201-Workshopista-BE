using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Repository.Consts;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.Models;
using Service.Models.Organizers;
using Service.Models.Transaction;
using Service.Models.Users;
using Service.Models.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Services.Organizers
{
    public class OrganizerService : IOrganizerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWorkshopService _workshopService;

        public OrganizerService(IUnitOfWork unitOfWork, IMapper mapper, IWorkshopService workshopService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _workshopService = workshopService;
        }

        public async Task<IEnumerable<Organizer>> GetAllOrganizesAsync()
        {
            return await _unitOfWork.Organizers.GetAllOrganizersAsync();
        }

        public async Task<ApiResponse<OrganizerDetailsDto>> GetOrganizeByIdAsync(string email)
        {
            var existingUser = await _unitOfWork.Users.GetUserByUserNameAsync(email);

            if (existingUser == null)
            {
                throw new CustomException(ResponseMessage.UserNotFound);
            }

            if (existingUser.Organizers.Count != 1)
            {
                throw new CustomException(ResponseMessage.OrganizerNotFound);
            }

            return ApiResponse<OrganizerDetailsDto>.SuccessResponse(_mapper.Map<OrganizerDetailsDto>(existingUser.Organizers.FirstOrDefault()));
        }

        public async Task UpdateOrganizerAsync(UpdateOrganizerModel model, string email)
        {
            var existingUser = await _unitOfWork.Users.GetUserByUserNameAsync(email);
            if (existingUser == null)
            {
                throw new CustomException(ResponseMessage.UserNotFound);
            }

            var organizers = existingUser.Organizers;

            if (organizers.Count > 1)
            {
                throw new CustomException(ResponseMessage.OrganizerNotFound);
            }

            var organizer = organizers.FirstOrDefault() ?? new Organizer
            {
                CreatedAt = DateTime.Now,
            };

            organizer.OrganizationName = model.OrganizationName;
            organizer.ContactPhone = model.ContactPhone;
            organizer.ContactEmail = model.ContactEmail;
            organizer.SocialLinks = string.IsNullOrEmpty(model.SocialLinks) ? organizer.SocialLinks : model.SocialLinks;
            organizer.WebsiteUrl = string.IsNullOrEmpty(model.WebsiteUrl) ? organizer.WebsiteUrl : model.WebsiteUrl;
            organizer.UpdatedAt = DateTime.Now;

            if (organizers.Count == 0)
            {
                existingUser.Organizers.Add(organizer);
            }

            await _unitOfWork.Users.Update(existingUser);
        }


        public async Task DeleteOrganizerAsync(Guid organizerId)
        {
            await _unitOfWork.Organizers.DeleteOrganizerAsync(organizerId);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string token)
        {
            return await _unitOfWork.Organizers.GetOrganizerByRefreshToken(token);
        }

        public async Task<ApiResponse<Organizer>> CreateOrganizerAsync(OrganizerCreateModel createModel, string email)
        {
            var existingUser = await _unitOfWork.Users.GetUserByUserNameAsync(email);

            if (existingUser == null)
            {
                throw new CustomException(ResponseMessage.UserNotFound);
            }

            if (existingUser.Organizers.Count != 0)
            {
                throw new CustomException(ResponseMessage.OrganizerFound);
            }

            Organizer organizer = new Organizer();
            _mapper.Map(createModel, organizer);
            existingUser.Organizers.Add(organizer);

            await _unitOfWork.CompleteAsync();

            return ApiResponse<Organizer>.SuccessResponse(organizer);
        }

        public async Task ChangeStatus(Guid organizerId, string status)
        {
            var organizer = await _unitOfWork.Organizers.GetOrganizerByIdAsync(organizerId);
            if (organizer == null)
            {
                throw new CustomException("The organizer is not exist!");
            }
            organizer.Status = status;

            await _unitOfWork.Organizers.UpdateOrganizerAsync(organizer);

        }

        public async Task<ApiResponse<WorkShopResponseModelWithPagination>> GetOrganizerWorkshop(string email, WorkshopFilterModel filters)
        {
            var existingUser = await _unitOfWork.Users.GetUserByUserNameAsync(email);

            if (existingUser == null)
            {
                throw new CustomException(ResponseMessage.UserNotFound);
            }

            if (existingUser.Organizers.Count != 1)
            {
                throw new CustomException(ResponseMessage.OrganizerNotFound);
            }

            var allWorkshop = await _workshopService.GetAll();

            if (allWorkshop.Data == null || allWorkshop.Data.Count() == 0)
            {
                return ApiResponse<WorkShopResponseModelWithPagination>.SuccessResponse(new WorkShopResponseModelWithPagination
                {
                    workShops = new List<WorkShopResponseModel>(),
                    CurrentPage = 1,
                    Total = 0
                });
            }

            var organizerWorkshop = allWorkshop.Data
                .Where(x => x.Organizer.OrganizerId == existingUser.Organizers.FirstOrDefault().OrganizerId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filters.Title))
                organizerWorkshop = organizerWorkshop.Where(w => w.Title.Contains(filters.Title));
            if (filters.CategoryId.HasValue)
                organizerWorkshop = organizerWorkshop.Where(w => w.CategoryId == filters.CategoryId);
            if (!string.IsNullOrEmpty(filters.LocationCity))
                organizerWorkshop = organizerWorkshop.Where(w => w.LocationCity == filters.LocationCity);
            if (filters.Price.HasValue)
                organizerWorkshop = organizerWorkshop.Where(w => w.Price == filters.Price);
            if (filters.StartTime.HasValue)
                organizerWorkshop = organizerWorkshop.Where(w => w.StartTime >= filters.StartTime);
            if (filters.EndTime.HasValue)
                organizerWorkshop = organizerWorkshop.Where(w => w.EndTime <= filters.EndTime);
            if (filters.Status.HasValue)
            {
                string currStatus = filters.Status > 0 ? StatusConst.Active : StatusConst.InActive;
                organizerWorkshop = organizerWorkshop.Where(w => w.Status == currStatus);
            }

            var result = organizerWorkshop.ToList();

            return ApiResponse<WorkShopResponseModelWithPagination>.SuccessResponse(new WorkShopResponseModelWithPagination
            {
                workShops = result
                    .Skip((filters.Page - 1) * filters.PageSize)
                    .Take(filters.PageSize)
                    .ToList(),
                CurrentPage = filters.Page,
                Total = result.Count()
            });
        }

        public async Task<TransactionStatisticModel> GetRevenueStatisticOfWorkshop(Guid organizerId)
        {
            var orderList = await _unitOfWork.Orders.GetCompletedOrderOfOrganizer(organizerId);
            var allTimes = orderList.Sum(o => o.TotalAmount) * 90 / 100;
            var month = orderList.Where(o => DateTime.Now.Month == o.CreatedAt.Value.Month).Sum(o => o.TotalAmount) * 90 / 100;
            var days = orderList.Where(o => DateTime.Now.Subtract(o.CreatedAt.Value).TotalDays <= 7).Sum(o => o.TotalAmount) * 90 / 100;

            return new TransactionStatisticModel
            {
                TotalAmount = allTimes.Value,
                MonthAmount = month.Value,
                SevenDaysAmount = days.Value,
            };
        }
    }
}
