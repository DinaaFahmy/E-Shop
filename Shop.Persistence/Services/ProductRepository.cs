using Microsoft.EntityFrameworkCore;
using Shop.Models.Models;
using Shop.Models.Wrappers;
using Shop.Persistence.Data;
using Shop.Persistence.Extensions;
using Shop.Persistence.Interfaces;
using Shop.ViewModels.Wrappers;

namespace Shop.Persistence.Services
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Page<Product>> GetFilteredProducts(PaginationRequest paginationRequest, long? categoryId)
        {
            if (categoryId == null)
                return await _dbContext.Set<Product>().AsNoTracking().GetPaged(paginationRequest);
            else
                return await _dbContext.Set<Product>().Where(a => a.CategoryId == categoryId).AsNoTracking().GetPaged(paginationRequest);
        }

        public List<Product> GetFilteredProducts(long? categoryId)
        {
            if (categoryId == null)
                return _dbContext.Set<Product>().AsNoTracking().ToList();
            else
                return _dbContext.Set<Product>().Where(a => a.CategoryId == categoryId).AsNoTracking().ToList();
        }
    }
}
