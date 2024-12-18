﻿using AutoMapper;
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
        private readonly IFirebaseStorageService _firebaseStorageService;

        public WorkshopService(IUnitOfWork unitOfWork, IMapper mapper, IFirebaseStorageService firebaseStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebaseStorageService = firebaseStorageService;
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

        public async Task<ApiResponse<IEnumerable<WorkShopResponseModel>>> GetAll()
        {
             var query = _unitOfWork.Workshops.Get();
             var workshops = await query
                .Include(x => x.Organizer)
                    .ThenInclude(x => x.User)
                .Include(x => x.TicketRanks)
                .Include(x => x.WorkshopImages)
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
            if (existingUser.Organizers.Count != 1)
            {
                throw new CustomException(ResponseMessage.OrganizerNotFound);
            }
            var organizer = existingUser.Organizers.FirstOrDefault();

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
            workshop.StartTime = DateTimeOffset.FromUnixTimeSeconds(workshopCreateDto.StartTime).UtcDateTime;
            workshop.EndTime = DateTimeOffset.FromUnixTimeSeconds(workshopCreateDto.EndTime).UtcDateTime;
            workshop.Capacity = ticketRank.Sum(x => x.Capacity);
            workshop.Price = ticketRank.Min(x => x.Price);

            var downloadUrl = await _firebaseStorageService.UploadFile(workshopCreateDto.WorkshopImages);

            foreach (var link in downloadUrl)
            {
                var workshopImage = new WorkshopImage
                {
                    ImageId = Guid.NewGuid(),
                    WorkshopId = workshop.WorkshopId,
                    ImageUrl = link,
                    IsPrimary = false,
                    CreatedAt = DateTime.UtcNow
                };

                workshop.WorkshopImages.Add(workshopImage);
            }

            await _unitOfWork.Workshops.Add(workshop);

            var workshopDto = _mapper.Map<WorkShopResponseModel>(workshop);
            return ApiResponse<WorkShopResponseModel>.SuccessResponse(workshopDto, ResponseMessage.CreateSuccess);
        }

        public async Task<ApiResponse<bool>> DeleteWorkshop(string id)
        {
            Guid workShopId = Guid.Parse(id);
            var existingWorkshop = _unitOfWork.Workshops.GetById(workShopId);
            if (existingWorkshop == null)
            {
                throw new CustomException(ResponseMessage.WorkshopNotFound + ResponseMessage.FromRequestModel);
            }
            existingWorkshop.Status = StatusConst.InActive;
            await _unitOfWork.Workshops.Update(existingWorkshop);

            return ApiResponse<bool>.SuccessResponse(true, ResponseMessage.DeleteSuccess);
        }

        public async Task<ApiResponse<bool>> UpdateWorkshopImageStatus(string imageId)
        {
            Guid ImageId = Guid.Parse(imageId);
            var query = _unitOfWork.WorkshopImage.GetWorkshopImages();

            var existingImage = await query
                .FirstOrDefaultAsync(x => x.ImageId == ImageId);

            if (existingImage == null)
                throw new CustomException(ResponseMessage.ImageNotFound);

            if (existingImage.WorkshopId == null)
                throw new CustomException(ResponseMessage.WorkshopNotFound);

            var workshop = await _unitOfWork.Workshops.Get()
                .Include(x => x.WorkshopImages)
                .FirstOrDefaultAsync(x => x.WorkshopId == (Guid)existingImage.WorkshopId);

            if (workshop == null)
                throw new CustomException(ResponseMessage.WorkshopNotFound);

            // Step 4: Set all images in the same workshop to false for IsPrimary
            foreach (var image in workshop.WorkshopImages)
            {
                image.IsPrimary = false;
            }

            existingImage.IsPrimary = true;

            await _unitOfWork.WorkshopImage.Update(existingImage);

            await _unitOfWork.Workshops.Update(workshop);

            return ApiResponse<bool>.SuccessResponse(true, ResponseMessage.UpdateSuccess);
        }

        public async Task<ApiResponse<List<WorkshopImageResponseModel>>> GetWorkShopBanner()
        {
            var query1 = _unitOfWork.Promotions.GetQuery();
            var promotionWorkshopIds = query1
                      .Where(p => p.StartDate <= DateTime.UtcNow
                            && p.EndDate >= DateTime.UtcNow
                            && p.Workshop.Status == StatusConst.Active
                            && p.PromotionType == PromotionConstants.Banner)
                      .Select(p => p.WorkshopId)
                      .ToList();

            var query2 = _unitOfWork.WorkshopImage.GetWorkshopImages();
            var workshopImages = await query2
                    .Where(img => promotionWorkshopIds.Contains(img.WorkshopId.Value) && img.IsPrimary == true)
                    .ToListAsync();

            if (workshopImages.Count == 0)
            {
                return ApiResponse<List<WorkshopImageResponseModel>>.SuccessResponse(new List<WorkshopImageResponseModel>());
            }

            var responseModel = _mapper.Map<List<WorkshopImageResponseModel>>(workshopImages);

            return ApiResponse<List<WorkshopImageResponseModel>>.SuccessResponse(responseModel, ResponseMessage.ReadSuccess);
        }

        public async Task<ApiResponse<WorkShopResponseModel>> UpdateWorkshop(WorkShopUpdateRequestModel workshopUpdateDto, string id)
        {
            // Fetch the workshop by id
            var query = _unitOfWork.Workshops.Get();
            var existingWorkshop = query
                                .Include(x => x.Category)
                                .Include(x => x.TicketRanks)
                                .Include(x => x.Organizer)
                                .Include(x => x.WorkshopImages)
                                .FirstOrDefault(x => x.WorkshopId == Guid.Parse(id));
            if (existingWorkshop == null)
            {
                throw new CustomException(ResponseMessage.WorkshopNotFound + ResponseMessage.FromRequestModel);
            }

            if (existingWorkshop.Status == StatusConst.Active)
            {
                throw new CustomException("Cannot update information for an active workshop.");
            }
            _mapper.Map(workshopUpdateDto, existingWorkshop);

            if (workshopUpdateDto.WorkshopImages.Count > 0)
            {
                existingWorkshop.WorkshopImages.Clear();
                var downloadUrl = await _firebaseStorageService.UploadFile(workshopUpdateDto.WorkshopImages);

                foreach (var link in downloadUrl)
                {
                    var workshopImage = new WorkshopImage
                    {
                        ImageId = Guid.NewGuid(),
                        WorkshopId = Guid.Parse(id),
                        ImageUrl = link,
                        IsPrimary = false,
                        CreatedAt = DateTime.UtcNow
                    };

                    existingWorkshop.WorkshopImages.Add(workshopImage);
                }
            }


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

            if (workshopUpdateDto.StartTime.HasValue)
            {
                DateTime newStartTime = DateTimeOffset.FromUnixTimeSeconds(workshopUpdateDto.StartTime
                    ?? throw new CustomException("StartTime is null but still access the if loop.")).UtcDateTime;

                // Check if the newStartTime is valid compared to existing EndTime
                if (newStartTime >= existingWorkshop.EndTime)
                {
                    throw new CustomException("New StartTime cannot be equal to or after the existing EndTime.");
                }

                // If valid, update the StartTime
                existingWorkshop.StartTime = newStartTime;
            }

            if (workshopUpdateDto.EndTime.HasValue)
            {
                DateTime newEndTime = DateTimeOffset.FromUnixTimeSeconds(workshopUpdateDto.EndTime
                    ?? throw new CustomException("EndTime is null but still accessed the if loop.")).UtcDateTime;

                // Check if the newEndTime is valid compared to existing StartTime
                if (newEndTime <= existingWorkshop.StartTime)
                {
                    throw new CustomException("New EndTime cannot be equal to or before the existing StartTime.");
                }

                // If valid, update the EndTime
                existingWorkshop.EndTime = newEndTime;
            }

            if (workshopUpdateDto.TicketRanks != null && workshopUpdateDto.TicketRanks.Any())
            {
                // Update ticket ranks logic
                var updatedTicketRanks = _mapper.Map<List<TicketRank>>(workshopUpdateDto.TicketRanks);
                existingWorkshop.TicketRanks.Clear();
                existingWorkshop.TicketRanks = updatedTicketRanks;
                existingWorkshop.Capacity = updatedTicketRanks.Sum(x => x.Capacity);
                existingWorkshop.Price = updatedTicketRanks.Min(x => x.Price);
            }

            await _unitOfWork.Workshops.Update(existingWorkshop);

            var updatedWorkshopDto = _mapper.Map<WorkShopResponseModel>(existingWorkshop);
            return ApiResponse<WorkShopResponseModel>.SuccessResponse(updatedWorkshopDto, ResponseMessage.UpdateSuccess);
        }

    }
}
