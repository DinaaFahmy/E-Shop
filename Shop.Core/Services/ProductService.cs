using AutoMapper;
using Shop.Persistence.Interfaces;
using Shop.Models.Models;
using Shop.ViewModels.Request;
using Shop.ViewModels.Response;
using Shop.Models.Wrappers;
using Shop.ViewModels.Wrappers;

namespace Shop.Core.Services
{
    public class ProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> CreateProduct(CreateProductRequest request)
        {
            var category = _mapper.Map<Product>(request);

            var result = await _productRepository.Add(category);

            return _mapper.Map<ProductResponse>(result);
        }

        public async Task<Page<ProductResponse>> GetFilteredProducts(PaginationRequest paginationRequest, long? categoryId)
        {
            var result = await _productRepository.GetFilteredProducts(paginationRequest, categoryId);
            return _mapper.Map<Page<ProductResponse>>(result);
        }
    }
}
