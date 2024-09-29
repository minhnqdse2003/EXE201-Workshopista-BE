using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Consts;
using Repository.Helpers;
using Service.Interfaces.IAuth;
using Service.Models;
using Service.Models.Auth;
using Service.Models.Token;
using Service.Models.Users;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("user-register")]
        public async Task<IActionResult> RegisterUser(UserRegisterModel model)
        {
            await _authService.RegisterAccount(model);
            return Ok("Register account successfully!");
        }

        [HttpPost("organizer-register")]
        public async Task<IActionResult> CreateOrganizer(OrganizerRegisterModel model)
        {
            await _authService.RegisterOrganizerAccount(model);
            return Ok("Register account successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginReq)
        {
            var result = await _authService.Login(loginReq);
            if (result.Message == ResponseMessage.InvalidLogin)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpGet]
        [Authorize(Roles = RoleConst.Organizer)]
        public async Task<IActionResult> Authenticate()
        {
            return Ok();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] Token token)
        {
            var result = await _authService.RefreshToken(token.refreshToken);
            if (result.Message == ResponseMessage.Unauthorized)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
