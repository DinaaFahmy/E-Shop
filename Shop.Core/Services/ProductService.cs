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
            var product = _mapper.Map<Product>(request);

            var result = await _productRepository.Add(product);

            return _mapper.Map<ProductResponse>(result);
        }

        public async Task<ProductResponse> GetById(long id)
        {
            var result = await _productRepository.GetById(id);

            return _mapper.Map<ProductResponse>(result);
        }

        public async Task<Page<ProductResponse>> GetFilteredProducts(PaginationRequest paginationRequest, long? categoryId)
        {
            var result = await _productRepository.GetFilteredProducts(paginationRequest, categoryId);
            return _mapper.Map<Page<ProductResponse>>(result);
        }

        public List<ProductResponse> GetFilteredProducts(long? categoryId)
        {
            var result = _productRepository.GetFilteredProducts(categoryId);
            return _mapper.Map<List<ProductResponse>>(result);
        }
    }
}
