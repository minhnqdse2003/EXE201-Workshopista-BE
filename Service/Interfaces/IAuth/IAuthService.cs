using Repository.Models;
using Service.Models;
using Service.Models.Auth;
using Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.IAuth
{
    public interface IAuthService
    {
        Task<ApiResponse<User>> Authenticate(string username, string password);

        string GenerateJwtToken(User user);

        string DecodeToken(string token, string nameClaim);

        string GenerateRefreshToken();

        Task<ApiResponse<TokenModel>> RefreshToken(string token);

        Task<ApiResponse<TokenModel>> Login(LoginModel loginReq);

        Task RegisterAccount(UserRegisterModel model);

        Task RegisterOrganizerAccount(OrganizerRegisterModel model);
    }
}
