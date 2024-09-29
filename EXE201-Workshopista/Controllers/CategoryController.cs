using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.ICategory;
using Service.Models.Category;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategories();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var result = await _categoryService.GetCategory(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequestModel model)
        {
            await _categoryService.CreateCategory(model);
            return Ok("Create category successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(CategoryRequestModel model, Guid id)
        {
            await _categoryService.UpdateCategory(id, model);
            return Ok("Update category successfully!");
        }

        [HttpPut("{id}/soft-remove")]
        public async Task<IActionResult> SoftRemoveCategory(Guid id, string status)
        {
            await _categoryService.SoftRemove(id, status);
            return Ok("Soft remove successfully!");
        }
    }
}
