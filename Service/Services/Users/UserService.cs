using AutoMapper;
using Repository.Consts;
using Microsoft.AspNetCore.SignalR.Protocol;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.Interfaces.IEmailService;
using Service.Models;
using Service.Models.Users;
using Service.Ultis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _unitOfWork.Users.GetUserByIdAsync(userId);
        }

        public async Task CreateUserAsync(PostUserModel user)
        {
            User userMappingModel = _mapper.Map<User>(user);
            userMappingModel.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Users.CreateUserAsync(userMappingModel);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _unitOfWork.Users.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            await _unitOfWork.Users.DeleteUserAsync(userId);
        }

        public async Task<User> GetUserByRefreshTokenAsync(string token)
        {
            return await _unitOfWork.Users.GetUserByRefreshToken(token);
        }



        public async Task ChangeStatus(Guid userId, string status)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new CustomException("The user is not exist!");
            }
            user.Status = status;

            await _unitOfWork.Users.UpdateUserAsync(user);

        }

        public async Task<UserInformationModel> GetOwnInformation(string token)
        {
            var userEmail = JwtDecode.DecodeToken(token, ClaimTypes.Email);
            var user = await _unitOfWork.Users.GetUserByEmail(userEmail);

            if (user == null)
            {
                throw new CustomException("The user is not exist!");
            }

            var response = _mapper.Map<UserInformationModel>(user);
            return response;
        }

        public async Task<UserListAndCount> GetAllParticipant()
        {
            var result = await _unitOfWork.Users.GetListUser();
            return new UserListAndCount
            {
                Users = result,
                Count = result.Count
            };
        }

        public async Task<UserListAndCount> GetAllOrganizer()
        {
            var result = await _unitOfWork.Users.GetListOrganizer();
            return new UserListAndCount
            {
                Users = result,
                Count = result.Count
            };
        }
    }
}
