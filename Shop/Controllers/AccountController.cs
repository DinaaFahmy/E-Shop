using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;
using Shop.ViewModels.Request;

namespace Shop.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [Route("")]
        public IActionResult index()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            var account = _authService.Login(new LoginRequest { Email = email, Password = password }).Result;
            if (account != null)
            {
                return Redirect("Products/Index");
            }
            else
            {
                ViewBag.msg = "Invalid Login";
                return View();
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(string email, string password, string username, string address, string phone, DateTime birthDate)
        {
            var account = _authService.Register(new RegisterRequest { Email = email, Password = password, Address = address, BirthDate = birthDate, PhoneNumber = phone, UserName = username }).Result;
            if (account != null)
            {
                return Redirect("Products/Index");
            }
            else
            {
                ViewBag.msg = "Invalid Info";
                return View();
            }
        }
    }
}
