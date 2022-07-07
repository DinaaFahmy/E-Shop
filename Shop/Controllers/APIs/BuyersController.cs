using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Shop.ViewModels.Wrappers;

namespace Shop.Controllers.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        private readonly BuyersService _buyersService;

        public BuyersController(BuyersService buyersService)
        {
            _buyersService = buyersService;
        }

        [HttpGet]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> GetPage([FromQuery] PaginationRequest paginationRequest)
        {
            var result = await _buyersService.GetPage(paginationRequest);
            return Ok(result);
        }

    }
}
