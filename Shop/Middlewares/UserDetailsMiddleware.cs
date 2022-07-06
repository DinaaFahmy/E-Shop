using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Profile = Shop.Models.Models.Profile;
using Shop.Persistence.Interfaces;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;

namespace Shop.Middlewares
{
    public class UserDetailsMiddleware
    {
        private readonly RequestDelegate _next;
        public UserDetailsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context,
                                      UserManager<Profile> userManager,
                                      IProfileRepository userRepository,
                                      IWebHostEnvironment environment,
                                      IOptions<Models.AppSettings> appSettingsOptions)
        {
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                await _next(context);
                return;
            }

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRoles = context.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("Unauthorized");

            if (string.IsNullOrEmpty(user.Email))
                throw new Exception("Invalid Login");

            var accessToken = context.Request.Headers[HeaderNames.Authorization][0].Split(" ")[1];

            await _next(context);
            return;
        }
    }
}
