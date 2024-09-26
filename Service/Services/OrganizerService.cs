using AutoMapper;
using Repository.Consts;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.Models.Organizers;
using Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
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

        public async Task<Organizer> GetOrganizeByIdAsync(Guid organizerId)
        {
            return await _unitOfWork.Organizers.GetOrganizerByIdAsync(organizerId);
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

        public async Task RegisterOrganizerAccount(OrganizerRegisterModel model)
        {
            var exist = await _unitOfWork.Users.GetUserByEmail(model.Email);
            if (exist != null)
            {
                throw new CustomException("The email has already registered by other account!");
            }

            var organizerExist = await _unitOfWork.Organizers.GetOrganizerByEmail(model.ContactEmail);
            if (organizerExist != null)
            {
                throw new CustomException("The organization's email has already existed!");
            }

            User newUser = _mapper.Map<User>(model);
            Organizer newOrganizer = _mapper.Map<Organizer>(model);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            newUser.Role = RoleConst.Organizer;
            newUser.CreatedAt = DateTime.Now;

            newOrganizer.CreatedAt = DateTime.Now;
            newOrganizer.UserId = newUser.UserId;
            await _unitOfWork.Users.CreateUserAsync(newUser);
            await _unitOfWork.Organizers.Add(newOrganizer);
            _unitOfWork.Complete();
        }
    }
}
