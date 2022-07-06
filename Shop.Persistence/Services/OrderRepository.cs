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
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Page<Order>> GetFilteredOrders(PaginationRequest paginationRequest, long? userId)
        {
            if (userId.Value == null)
                return await _dbContext.Set<Order>().AsNoTracking().GetPaged(paginationRequest);
            else
                return await _dbContext.Set<Order>().Where(a => a.CreatedBy == userId).AsNoTracking().GetPaged(paginationRequest);
        }
    }
}
