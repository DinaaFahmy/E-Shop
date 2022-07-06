using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.ViewModels.Request;
using Shop.ViewModels.Response;
using Profile = Shop.Models.Models.Profile;
using System.Security.Claims;
using Shop.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shop.Core.Services
{
    public class AuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Profile> _userManager;
        private readonly AppSettings _appSettings;

        public AuthService(IMapper mapper, UserManager<Profile> userManager, IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<ProfileResponse> Register(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception("This Email already exists");

            var newUser = _mapper.Map<Profile>(request);

            var identityResult = await _userManager.CreateAsync(newUser, request.Password);
            //await _userManager.AddToRoleAsync(newUser, "Customer");

            if (!identityResult.Succeeded)
                throw new Exception(identityResult.Errors.First().Description);

            return _mapper.Map<ProfileResponse>(newUser);
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
                throw new Exception("Invalid Login");

            var passwordResult = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!passwordResult)
                throw new Exception("Invalid Login");

            var userRoles = await _userManager.GetRolesAsync(user);

            //set claims for the token
            var authClaims = new List<Claim>()
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(_appSettings.JWTTokenDuration.Days)
                                     .AddHours(_appSettings.JWTTokenDuration.Hours)
                                     .AddMinutes(_appSettings.JWTTokenDuration.Minutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWTSecretKey)),
                                                           SecurityAlgorithms.HmacSha256)
            );

            return new LoginResponse()
            {
                User = _mapper.Map<ProfileResponse>(user),
                UserRoles = userRoles.ToList(),

                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }
}
