using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Consts;
using Repository.Models;
using Service.Interfaces;
using Service.Models.Workshops;
using Service.Services;
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

        [HttpGet("GetFilter")]
        public async Task<IActionResult> Get([FromQuery] WorkshopFilterModel filterModel)
        {
            var result = await _workshopService.GetFilter(filterModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _workshopService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] WorkShopCreateRequestModel createRequestModel)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            var result = await _workshopService.AddWorkshop(createRequestModel, email);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            return Ok(_workshopService.GetWorkshopById(Guid.Parse(id)));
        }

        [HttpGet]
        [Route($"{nameof(WorkshopImage)}/{PromotionConstants.Banner}")]
        public async Task<IActionResult> GetWorkshopBannerAsync()
        {
            var result = await _workshopService.GetWorkShopBanner();
            return Ok(result);
        }

        [HttpPut]
        [Route($"{nameof(WorkshopImage)}" + "/{id}")]
        public async Task<IActionResult> UpdateWorkshopImageStatus([FromRoute] string id)
        {
            var result = await _workshopService.UpdateWorkshopImageStatus(id);
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            return Ok(_workshopService.DeleteWorkshop(id));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(WorkShopUpdateRequestModel updateRequestModel, [FromRoute] string id)
        {
            return Ok(await _workshopService.UpdateWorkshop(updateRequestModel, id));
        }

        [HttpGet]
        [Route("list/{organizerId}")]
        public async Task<IActionResult> GetWorkshopsByOrganizerId(Guid organizerId)
        {
            var result = await _workshopService.GetWorkshopListByOrganizerId(organizerId);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/statistic")]
        public async Task<IActionResult> GetWorkshopTicketStatistic(Guid id)
        {
            var result = await _workshopService.GetTicketStatistic(id);
            return Ok(result);
        }
    }
}
