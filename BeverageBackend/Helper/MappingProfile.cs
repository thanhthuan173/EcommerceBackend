using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Dto.Auth;
using BeverageBackend.Dto.Cart;
using BeverageBackend.Models;

namespace BeverageBackend.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //product
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            //category
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            //user
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<RegisterDto, User>();

            //cart
            CreateMap<Cart, CartDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<AddCartItemDto,CartItem>();

            //order
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
