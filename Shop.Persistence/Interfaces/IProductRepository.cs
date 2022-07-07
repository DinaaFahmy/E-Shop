using Shop.Models.Models;
using Shop.Models.Wrappers;
using Shop.ViewModels.Wrappers;

namespace Shop.Persistence.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Page<Product>> GetFilteredProducts(PaginationRequest paginationRequest, long? categoryId);
        List<Product> GetFilteredProducts(long? categoryId);
    }
}
