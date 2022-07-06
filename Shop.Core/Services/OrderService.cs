using AutoMapper;
using Shop.Persistence.Interfaces;
using Shop.Models.Models;
using Shop.ViewModels.Request;
using Shop.ViewModels.Response;
using Shop.Models.Wrappers;
using Shop.ViewModels.Wrappers;

namespace Shop.Core.Services
{
    public class OrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderResponse> CreateOrder(CreateOrderRequest request)
        {
            var order = _mapper.Map<Order>(request);

            var product = await _productRepository.GetById(request.ProductId);

            if (product.PriceForTwoPieces != null && product.PriceForTwoPieces > 0 && request.Quantity == 2)
            {
                order.TotalPrice = product.PriceForTwoPieces.Value;
            }

            else if (product.DiscountPercentage != null && product.DiscountPercentage > 0)
            {
                var productModifPrice = product.Price - (product.Price * (product.DiscountPercentage.Value / 100));
                order.TotalPrice = productModifPrice * request.Quantity;
            }

            else
            {
                order.TotalPrice = product.Price * request.Quantity;
            }

            var result = await _orderRepository.Add(order);

            return _mapper.Map<OrderResponse>(result);
        }

        public async Task<Page<OrderResponse>> GetFilteredOrders(PaginationRequest paginationRequest, long? userId)
        {
            var result = await _orderRepository.GetFilteredOrders(paginationRequest, userId);
            return _mapper.Map<Page<OrderResponse>>(result);
        }
    }
}
