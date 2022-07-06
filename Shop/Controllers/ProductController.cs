using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Shop.ViewModels.Request;
using Shop.ViewModels.Wrappers;

namespace Shop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var result = await _productService.CreateProduct(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredProducts([FromQuery] PaginationRequest paginationRequest, [FromQuery] long? categoryId)
        {
            var result = await _productService.GetFilteredProducts(paginationRequest, categoryId);
            return Ok(result);
        }

    }
}
