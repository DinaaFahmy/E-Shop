using Shop.Models.Models;
using Shop.Models.Wrappers;
using Shop.Persistence.Data;
using Shop.Persistence.Extensions;
using Shop.Persistence.Interfaces;
using Shop.ViewModels.Wrappers;
using Microsoft.AspNetCore.Identity;
using Constants;

namespace Shop.Persistence.Services
{
    public class ProfileRepository : IProfileRepository
    {
        public ApplicationDbContext _dbContext;
        private readonly UserManager<Profile> _userManager;

        public ProfileRepository(ApplicationDbContext dbContext, UserManager<Profile> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Page<Profile>> GetPage(PaginationRequest pageRequest)
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Customer);
            return await users.GetPaged(pageRequest);
        }
    }
}
