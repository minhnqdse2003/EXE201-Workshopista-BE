using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            user.UpdatedAt = DateTime.UtcNow;
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
    }
}
