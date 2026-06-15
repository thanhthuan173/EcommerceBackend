using AutoMapper;
using BeverageBackend.Application.Dto.User;
using BeverageBackend.Application.Dto.Auth;
using BeverageBackend.Application.Dto.Cart;
using BeverageBackend.Application.Dto.Order;
using BeverageBackend.Application.Dto.Product;
using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //product
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            //category
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CategoryWithProductsDto>();

            //user
            CreateMap<User, UserDto>();
            CreateMap<User, UserDetailDto>();
            CreateMap<User, ProfileDto>();
            CreateMap<RegisterDto, User>();

            //cart
            CreateMap<Cart, CartDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<AddCartItemDto, CartItem>();

            //order
            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderDetailDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
