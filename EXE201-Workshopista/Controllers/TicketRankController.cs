using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.ITicketRank;
using Service.Models.TicketRank;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/ticket-rank")]
    [ApiController]
    public class TicketRankController : ControllerBase
    {
        private readonly ITicketRankService _ticketRankService;

        public TicketRankController(ITicketRankService ticketRankService)
        {
            _ticketRankService = ticketRankService;
        }

        [HttpGet("{workshopId}/ranks")]
        public async Task<IActionResult> GetTicketRankOfWorkshop(Guid workshopId)
        {
            var result = await _ticketRankService.GetAllTicketRankOfWorkshop(workshopId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketRankById(Guid id)
        {
            var result = await _ticketRankService.GetTicketRankById(id);
            return Ok(result);
        }

        [HttpPost("{workshopId}")]
        public async Task<IActionResult> CreateTicketRank(Guid workshopId, TicketRankRequestModel model)
        {
            await _ticketRankService.CreateTicketRank(model, workshopId);
            return Ok("Create ticket rank successfully!");
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicketRank(Guid id, TicketRankRequestModel model)
        {
            await _ticketRankService.UpdateTicketRank(model, id);
            return Ok("Update ticket rank successfully!");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteTicketRank(Guid id)
        {
            await _ticketRankService.DeleteTicketRank(id);
            return Ok("Delete ticket rank successfully!");
        }
    }
}
