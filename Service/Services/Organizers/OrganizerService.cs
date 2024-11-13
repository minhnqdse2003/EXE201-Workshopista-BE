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

        public async Task UpdateOrganizerAsync(UpdateOrganizerModel model, Guid id)
        {
            var organize = await _unitOfWork.Organizers.GetOrganizerByIdAsync(id);
            if (organize == null)
            {
                throw new CustomException("The organization is not existed!");
            }
            var organizerExist = await _unitOfWork.Organizers.GetOrganizerByEmail(model.ContactEmail);
            if (organizerExist != null)
            {
                throw new CustomException("The organization's email has already existed!");
            }
            _mapper.Map(model, organize);
            organize.UpdatedAt = DateTime.Now;
            await _unitOfWork.Organizers.UpdateOrganizerAsync(organize);
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
