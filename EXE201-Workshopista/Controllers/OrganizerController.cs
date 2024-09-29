using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Interfaces;
using Service.Models.Organizers;
using Service.Models.Users;
using Service.Services;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/organizer")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly IOrganizerService _organizerService;

        public OrganizerController(IOrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrganizers()
        {
            var organizers = await _organizerService.GetAllOrganizesAsync();
            return Ok(organizers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrganizer(Guid id)
        {
            var organizer = await _organizerService.GetOrganizeByIdAsync(id);
            if (organizer == null)
            {
                return NotFound();
            }

            return Ok(organizer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UpdateOrganizerModel model)
        {
            await _organizerService.UpdateOrganizerAsync(model, id);
            return Ok("Update organization successfully!");
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOrganizer(Guid id)
        //{
        //    var organizer = await _organizerService.GetOrganizeByIdAsync(id);
        //    if (organizer == null)
        //    {
        //        return NotFound();
        //    }

        //    await _organizerService.DeleteOrganizerAsync(id);
        //    return NoContent();
        //}

        

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] string status)
        {
            await _organizerService.ChangeStatus(id, status);
            return Ok("Update status successfully!");
        }
    }
}
