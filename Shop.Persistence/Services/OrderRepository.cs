using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Models.Models;
using Shop.Models.Wrappers;
using Shop.Persistence.Data;
using Shop.Persistence.Extensions;
using Shop.Persistence.Interfaces;
using Shop.ViewModels.Wrappers;

namespace Shop.Persistence.Services
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly UserManager<Profile> _userManager;

        public OrderRepository(ApplicationDbContext dbContext, UserManager<Profile> userManager) : base(dbContext)
        {
            _userManager = userManager;
        }
        public async Task<Page<Order>> GetFilteredOrders(PaginationRequest paginationRequest, long? userId)
        {
            if (userId == null)
                return await _dbContext.Set<Order>().AsNoTracking().GetPaged(paginationRequest);
            else
                return await _dbContext.Set<Order>().Where(a => a.CreatedBy == userId).AsNoTracking().GetPaged(paginationRequest);
        }

        public async Task<List<Order>> GetFilteredOrders(string userEmail)
        {
            if (userEmail == null)
                return _dbContext.Set<Order>().AsNoTracking().ToList();
            else
            {
                var user = (await _userManager.FindByEmailAsync(userEmail)).Id;
                return _dbContext.Set<Order>().Where(a => a.CreatedBy.ToString() == user).AsNoTracking().ToList();

            }
        }
    }
}
