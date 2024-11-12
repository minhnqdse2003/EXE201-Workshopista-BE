using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models.Reviews;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [Route("{workshopId}")]
        public async Task<IActionResult> GetAllReviewOfWorkshop(Guid workshopId)
        {
            var list = await _reviewService.GetAllReviewOfWorkshop(workshopId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReviewForWorkshop(ReviewCreateModel model)
        {
            string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            await _reviewService.CreateReview(model, token);
            return Ok("Create review successfully!"); 
        }
    }
}
