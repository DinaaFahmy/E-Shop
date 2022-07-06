using Shop.Persistence.Interfaces;
using Shop.ViewModels.Wrappers;
using Shop.Models.Wrappers;
using Shop.ViewModels.Response;

namespace Shop.Core.Services
{
    public class BuyersService
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly IProfileRepository _profileRepository;

        public BuyersService(IProfileRepository profileRepository, AutoMapper.IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task<Page<ProfileResponse>> GetPage(PaginationRequest pageRequest)
        {
            var result = await _profileRepository.GetPage(pageRequest);
            return _mapper.Map<Page<ProfileResponse>>(result);
        }
    }
}