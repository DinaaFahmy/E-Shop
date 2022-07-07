using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Shop.ViewModels.Request;

namespace Shop.Controllers
{
    [Route("Categories")]
    public class CategoriesController : Controller
    {
        private readonly CategoryService _categoryService;
        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            ViewBag.categories = await _categoryService.GetCategories();
            return View();
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateCategoryRequest category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(category);
                return RedirectToAction("");
            }

            return View(category);
        }
    }
}
