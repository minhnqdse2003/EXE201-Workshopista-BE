using Repository.Models;
using Service.Models;
using Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(Guid userId);

        Task<User> GetUserByRefreshTokenAsync(string token);

        Task CreateUserAsync(PostUserModel user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(Guid userId);

        Task RegisterAccount(UserRegisterModel model);
    }
}
