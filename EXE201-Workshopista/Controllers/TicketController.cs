using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Interfaces;
using Service.Models.Ticket;
using System.Security.Claims;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserTickets()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _ticketService.GetUserTicket(email));
        }

        [HttpPost("validate")]
        public async Task<IActionResult> VerifiedTicket([FromBody] ValidateQrModel validateQrModel)
        {
            return Ok(await _ticketService.Verify(validateQrModel.QrContent));
        }
    }
}
