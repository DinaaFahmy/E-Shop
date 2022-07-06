using Microsoft.EntityFrameworkCore;
using Shop.Models.Wrappers;
using Shop.ViewModels.Wrappers;

namespace Shop.Persistence.Extensions
{
    public static class Pagination
    {
        public static async Task<Page<T>> GetPaged<T>(this IQueryable<T> query, PaginationRequest paginationRequest) where T : class
        {
            var skip = paginationRequest.PageNumber * paginationRequest.PageSize;
            var result = new Page<T>(await query.Skip(skip).Take(paginationRequest.PageSize).ToListAsync(), paginationRequest.PageNumber, paginationRequest.PageSize, query.Count());
            return result;
        }

        public static Task<Page<T>> GetPaged<T>(this IEnumerable<T> list, PaginationRequest paginationRequest) where T : class
        {
            var skip = paginationRequest.PageNumber * paginationRequest.PageSize;
            var result = new Page<T>(list.Skip(skip).Take(paginationRequest.PageSize).ToList(), paginationRequest.PageNumber, paginationRequest.PageSize, list.Count());
            return Task.FromResult(result);
        }

        public static Page<T> GetPaged<T>(this List<T> list, PaginationRequest paginationRequest) where T : class
        {
            var records = list.Skip(paginationRequest.PageNumber * paginationRequest.PageSize)
                                     .Take(paginationRequest.PageSize)
                                     .ToList();

            return new Page<T>(records, paginationRequest.PageNumber, paginationRequest.PageSize, list.Count());
        }
    }

}
