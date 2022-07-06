using AutoMapper;
using Shop.Persistence.Interfaces;
using Shop.Models.Models;
using Shop.ViewModels.Request;
using Shop.ViewModels.Response;

namespace Shop.Core.Services
{
    public class CategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);

            var result = await _categoryRepository.Add(category);

            return _mapper.Map<CategoryResponse>(result);
        }

        public async Task<List<CategoryResponse>> GetCategories()
        {
            var result = await _categoryRepository.GetAll();
            return _mapper.Map<List<CategoryResponse>>(result);
        }
    }
}
