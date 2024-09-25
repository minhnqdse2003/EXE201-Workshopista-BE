using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.Models;
using Service.Models.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<ApiResponse<User>> Authenticate(string username, string password)
        {
            var user = await _unitOfWork.Users.GetUserByUserNameAsync(username);
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                return ApiResponse<User>.ErrorResponse(ResponseMessage.InvalidLogin);
            }

            return ApiResponse<User>.SuccessResponse(user);
        }

        private bool VerifyPasswordHash(string password, string? passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string DecodeToken(string token, string nameClaim)
        {
            var _tokenHandler = new JwtSecurityTokenHandler();
            Claim? claim = _tokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(t => t.Type.ToString().Equals(nameClaim));
            return claim != null ? claim.Value : "Error!";
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<ApiResponse<TokenModel>> Login(LoginModel loginReq)
        {
            var auth = await Authenticate(loginReq.Username, loginReq.Password);
            if (!auth.Success)
            {
                return ApiResponse<TokenModel>.ErrorResponse(ResponseMessage.InvalidLogin);
            }

            var token = GenerateJwtToken(auth.Data);
            var refreshToken = GenerateRefreshToken();

            auth.Data.RefreshToken = refreshToken;
            auth.Data.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userService.UpdateUserAsync(auth.Data);

            return ApiResponse<TokenModel>.SuccessResponse(
                new TokenModel { Token = token, RefreshToken = refreshToken },
                ResponseMessage.LoginSuccess);
        }

        public async Task<ApiResponse<TokenModel>> RefreshToken(string token)
        {
            var user = await _userService.GetUserByRefreshTokenAsync(token);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return ApiResponse<TokenModel>.ErrorResponse(ResponseMessage.Unauthorized);
            }

            var jwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userService.UpdateUserAsync(user);

            return ApiResponse<TokenModel>.SuccessResponse(
                new TokenModel { Token = jwtToken, RefreshToken = newRefreshToken },
                ResponseMessage.TokenRefreshed);
        }
    }
}
