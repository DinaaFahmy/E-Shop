using Microsoft.EntityFrameworkCore;
using Shop.Models.Models.Base;
using Shop.Persistence.Data;
using Shop.Persistence.Interfaces;

namespace Shop.Persistence.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ISqlEntity
    {
        public ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T item)
        {
            item.CreatedDate = DateTime.UtcNow;

            item.LastModifiedDate = DateTime.UtcNow;

            await _dbContext.AddAsync(item);
            await SaveAsync();

            _dbContext.Entry(item).State = EntityState.Detached;

            return item;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var query = _dbContext.Set<T>().AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<T> GetById(long Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }

        public async Task<bool> HasAny()
        {
            return await _dbContext.Set<T>().AnyAsync();
        }

        protected async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
