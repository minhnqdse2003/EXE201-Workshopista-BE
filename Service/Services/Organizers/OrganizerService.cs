using AutoMapper;
using Repository.Consts;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.Models;
using Service.Models.Organizers;
using Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Organizers
{
    public class OrganizerService : IOrganizerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrganizerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<ApiResponse<Organizer>> CreateOrganizerAsync(OrganizerCreateModel createModel,string email)
        {
            var existingUser = await _unitOfWork.Users.GetUserByUserNameAsync(email);
            
            if(existingUser == null)
            {
                throw new CustomException(ResponseMessage.UserNotFound);
            }

            if(existingUser.Organizers.Count != 0)
            {
                throw new CustomException(ResponseMessage.OrganizerFound);
            }

            Organizer organizer = new Organizer();
            _mapper.Map(createModel,organizer);
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
    }
}
