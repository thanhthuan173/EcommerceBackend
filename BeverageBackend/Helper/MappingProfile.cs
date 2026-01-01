using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Models;

namespace BeverageBackend.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Cart, CartDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
