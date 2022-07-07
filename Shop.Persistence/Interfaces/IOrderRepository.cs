using Shop.Models.Models;
using Shop.Models.Wrappers;
using Shop.ViewModels.Wrappers;

namespace Shop.Persistence.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Page<Order>> GetFilteredOrders(PaginationRequest paginationRequest, long? userId);
        Task<List<Order>> GetFilteredOrders(string userEmail);
    }
}
