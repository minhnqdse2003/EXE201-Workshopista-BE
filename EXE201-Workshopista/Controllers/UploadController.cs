using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IFirebaseStorageService _imageService;

        public UploadController(IFirebaseStorageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(List<IFormFile> file)
        {
            var downloadUrl = await _imageService.UploadFile(file);
            return Ok(new { DownloadUrl = downloadUrl });
        }
    }
}
