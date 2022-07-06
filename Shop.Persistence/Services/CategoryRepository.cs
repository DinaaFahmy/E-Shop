using Shop.Models.Models;
using Shop.Persistence.Data;
using Shop.Persistence.Interfaces;

namespace Shop.Persistence.Services
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
