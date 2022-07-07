using Shop.Models.Models;
using Shop.Models.Wrappers;
using Shop.ViewModels.Wrappers;

namespace Shop.Persistence.Interfaces
{
    public interface IProfileRepository
    {
        Task<Page<Profile>> GetPage(PaginationRequest pageRequest);
        Task<List<Profile>> GetAll();
    }
}
