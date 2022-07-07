using Shop.Persistence.Interfaces;
using Shop.ViewModels.Wrappers;
using Shop.Models.Wrappers;
using Shop.ViewModels.Response;
using Microsoft.AspNetCore.Identity;
using Shop.Models.Models;
using Constants;

namespace Shop.Core.Services
{
    public class BuyersService
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly UserManager<Profile> _userManager;

        public BuyersService(IProfileRepository profileRepository, AutoMapper.IMapper mapper, UserManager<Profile> userManager)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Page<ProfileResponse>> GetPage(PaginationRequest pageRequest)
        {
            var result = await _profileRepository.GetPage(pageRequest);
            return _mapper.Map<Page<ProfileResponse>>(result);
        }

        public async Task<List<ProfileResponse>> GetAll()
        {
            var result = await _profileRepository.GetAll();
            return _mapper.Map<List<ProfileResponse>>(result);
        }
    }
}