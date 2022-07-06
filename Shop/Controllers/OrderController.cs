using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Shop.ViewModels.Request;
using Shop.ViewModels.Wrappers;

namespace Shop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _oderService;

        public OrderController(OrderService oderService)
        {
            _oderService = oderService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.Customer)]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderRequest request)
        {
            var result = await _oderService.CreateOrder(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> GetFilteredOrders([FromQuery]PaginationRequest paginationRequest, [FromQuery] long? userId)
        {
            var result = await _oderService.GetFilteredOrders(paginationRequest, userId);
            return Ok(result);
        }


    }
}
