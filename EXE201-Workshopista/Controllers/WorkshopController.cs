using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models.Workshops;
using Service.Services.Workshops;
using System.Security.Claims;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshopService _workshopService;

        public WorkshopController(IWorkshopService workshopService)
        {
            _workshopService = workshopService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]WorkshopFilterModel filterModel)
        {
            var result = await _workshopService.GetFilter(filterModel);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(WorkShopCreateRequestModel createRequestModel)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            var result = await _workshopService.AddWorkshop(createRequestModel,email);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            return Ok( _workshopService.GetWorkshopById(Guid.Parse(id)));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            return Ok();
        }
    }
}
