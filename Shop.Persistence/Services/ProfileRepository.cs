using Microsoft.EntityFrameworkCore;
using Shop.Models.Models;
using Shop.Models.Wrappers;
using Shop.Persistence.Data;
using Shop.Persistence.Extensions;
using Shop.Persistence.Interfaces;
using Shop.ViewModels.Wrappers;

namespace Shop.Persistence.Services
{
    public class ProfileRepository : IProfileRepository
    {
        public ApplicationDbContext _dbContext;
        public ProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Page<Profile>> GetPage(PaginationRequest pageRequest)
        {
            var query = _dbContext.Set<Profile>().AsNoTracking();
            return await query.GetPaged(pageRequest);
        }
    }
}
