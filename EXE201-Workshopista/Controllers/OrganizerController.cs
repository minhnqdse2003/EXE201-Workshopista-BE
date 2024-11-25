using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Interfaces;
using Service.Models.Organizers;
using Service.Models.Users;
using Service.Models.Workshops;
using Service.Services;
using System.Security.Claims;

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

        [HttpGet("details")]
        [Authorize]
        public async Task<ActionResult> GetOrganizer()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _organizerService.GetOrganizeByIdAsync(email));
        }

        [HttpGet($"{nameof(Workshop)}")]
        [Authorize]
        public async Task<ActionResult> GetOrganizerWorkshop([FromQuery] WorkshopFilterModel filters)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _organizerService.GetOrganizerWorkshop(email,filters));
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(UpdateOrganizerModel model)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            await _organizerService.UpdateOrganizerAsync(model, email);
            return Ok("Update organization successfully!");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostOrganizer([FromBody]OrganizerCreateModel request)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _organizerService.CreateOrganizerAsync(request,email));
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
