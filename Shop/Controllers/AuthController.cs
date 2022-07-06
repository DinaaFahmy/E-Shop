using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Shop.ViewModels.Request;

namespace Shop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.Register(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginViewModel)
        {
            var result = await _authService.Login(loginViewModel);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
        {
            var result = await _authService.RegisterAdmin(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SeedRoles()
        {
            await _authService.SeedRoles();
            return Ok();
        }
    }
}
