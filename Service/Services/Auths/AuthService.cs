using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Repository.Consts;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.Interfaces.IAuth;
using Service.Interfaces.IEmailService;
using Service.Models;
using Service.Models.Auth;
using Service.Models.Users;
using Service.Ultis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Auths
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IUserService userService, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<ApiResponse<User>> Authenticate(string username, string password)
        {
            var user = await _unitOfWork.Users.GetUserByUserNameAsync(username);
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash) || user.Status.Equals(StatusConst.InActive) || user.EmailVerified == false)
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
                new TokenModel { Token = token, RefreshToken = refreshToken, User = _mapper.Map<LoginUserResponseModel>(auth.Data) },
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

        public async Task RegisterAccount(UserRegisterModel model)
        {
            var exist = await _unitOfWork.Users.GetUserByEmail(model.Email);
            if (exist != null)
            {
                throw new CustomException("The email has already registered by other account!");
            }

            User newUser = _mapper.Map<User>(model);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            newUser.Role = RoleConst.Participant;
            newUser.Status = StatusConst.Active;
            newUser.CreatedAt = DateTime.Now;
            newUser.EmailVerified = false;
            var newOtp = OTPGeneration.CreateNewOTPCode();
            var htmlBody = EmailTemplate.VerifyEmailOTP(model.Email, newOtp);
            bool sendEmailSuccess = await _emailService.SendEmail(model.Email, "Verify Email", htmlBody);

            if (!sendEmailSuccess)
            {
                throw new CustomException("An error occurred while sending email!");
            }

            Otp newOTPCode = new Otp()
            {
                Id = Guid.NewGuid(),
                Code = newOtp,
                CreatedBy = newUser.UserId,
                CreatedAt = DateTime.Now,
                IsUsed = false,
            };

            await _unitOfWork.Users.CreateUserAsync(newUser);
            await _unitOfWork.OTPs.Add(newOTPCode);
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
            newUser.EmailVerified = false;
            newUser.Status = StatusConst.Active;

            var newOtp = OTPGeneration.CreateNewOTPCode();
            var htmlBody = EmailTemplate.VerifyEmailOTP(model.Email, newOtp);
            bool sendEmailSuccess = await _emailService.SendEmail(model.Email, "Verify Email", htmlBody);

            if (!sendEmailSuccess)
            {
                throw new CustomException("An error occurred while sending email!");
            }

            Otp newOTPCode = new Otp()
            {
                Id = Guid.NewGuid(),
                Code = newOtp,
                CreatedBy = newUser.UserId,
                CreatedAt = DateTime.Now,
                IsUsed = false,
            };

            newOrganizer.CreatedAt = DateTime.Now;
            newOrganizer.UserId = newUser.UserId;
            newOrganizer.Status = StatusConst.Active;
            await _unitOfWork.Users.CreateUserAsync(newUser);
            await _unitOfWork.Organizers.Add(newOrganizer);
            await _unitOfWork.OTPs.Add(newOTPCode);

        }
    }
}
