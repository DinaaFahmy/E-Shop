using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Shop.Models.Models;
using Shop.ViewModels.Request;

namespace Shop.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly OrderService _orderService;
        private readonly UserManager<Profile> _userManager;
        public ProductsController(ProductService productService, CategoryService categoryService, OrderService orderService, UserManager<Profile> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
            _userManager = userManager;
        }

        [Route("")]
        public async Task<IActionResult> Index(long? id)
        {
            ViewBag.products = _productService.GetFilteredProducts(id);
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
        public async Task<IActionResult> Create(CreateProductRequest product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(product);
                return RedirectToAction("");
            }

            return View(product);
        }

        [HttpGet]
        [Route("MakeOrder/{productId}")]
        public async Task<IActionResult> MakeOrder(long productId, string str = null)
        {
            ViewBag.product = await _productService.GetById(productId);
            return View();
        }

        [HttpPost]
        [Route("MakeOrder/{productId}")]
        public async Task<IActionResult> MakeOrder(long productId)
        {
            //var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            await _orderService.CreateOrder(new CreateOrderRequest { ProductId = productId/*, CreatedBy = user.Id*/});
            return RedirectToAction("");
        }
    }
}
