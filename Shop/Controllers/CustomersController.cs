using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;

namespace Shop.Controllers
{
    [Route("Customers")]
    public class CustomersController : Controller
    {
        private readonly BuyersService _buyersService;
        private readonly OrderService _orderService;

        public CustomersController(BuyersService buyersService, OrderService orderService)
        {
            _buyersService = buyersService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.users = await _buyersService.GetAll();
            return View();
        }

        [Route("Orders")]
        public async Task<IActionResult> Orders(string email)
        {
            ViewBag.orders = await _orderService.GetFilteredOrders(email);
            return View();
        }
    }
}
