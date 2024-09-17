using Repository.Models;
using Service.Models;
using Service.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<User>> Authenticate(string username, string password);
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
        Task<ApiResponse<TokenModel>> RefreshToken(string token);
        Task<ApiResponse<TokenModel>> Login(LoginModel loginReq);
    }
}
