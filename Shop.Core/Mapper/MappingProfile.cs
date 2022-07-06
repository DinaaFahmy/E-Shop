using Shop.ViewModels.Request;
using Shop.ViewModels.Response;
using Shop.Models.Models;
using Shop.Models.Wrappers;

namespace Shop.Core.Mapper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<Models.Models.Profile, ProfileResponse>();
            CreateMap<Page<Models.Models.Profile>, Page<ProfileResponse>>();
            CreateMap<RegisterRequest, Models.Models.Profile>();

            // Product
            CreateMap<Product, ProductResponse>();
            CreateMap<Page<Product>, Page<ProductResponse>>();
            CreateMap<CreateProductRequest, Product>();

            // Category
            CreateMap<Category, CategoryResponse>();
            CreateMap<CreateCategoryRequest, Category>();

            // Order
            CreateMap<Order, OrderResponse>();
            CreateMap<Page<Order>, Page<OrderResponse>>();
            CreateMap<CreateOrderRequest, Order>();
        }
    }
}
