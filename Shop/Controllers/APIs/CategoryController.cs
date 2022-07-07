using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Shop.ViewModels.Request;

namespace Shop.Controllers.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var result = await _categoryService.CreateCategory(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetCategories();
            return Ok(result);
        }
    }
}
