using Shop.Models.Models.Base;

namespace Shop.Persistence.Interfaces
{
    public interface IGenericRepository<T> where T : ISqlEntity
    {
        Task<T> GetById(long Id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T item);
        Task<bool> HasAny();
    }
}
