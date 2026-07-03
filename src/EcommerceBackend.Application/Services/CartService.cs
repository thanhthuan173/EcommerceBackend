using AutoMapper;
using EcommerceBackend.Application.Dto.Cart;
using EcommerceBackend.Application.Exceptions;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Application.Interfaces.Services;
using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CartService(ICartRepository cartRepo, IProductRepository productRepo, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        private async Task<Cart> GetCartOrThrow()
        {
            var userId = _currentUser.UserId;
            var cart = await _cartRepo.GetByUserWithItemsAsync(userId);
            if (cart == null)
                throw new NotFoundException("Cart not found");
            return cart;
        }

        public async Task AddToCartAsync(AddCartItemDto dto)
        {
            var cart = await GetCartOrThrow();
            var product = await _productRepo.GetByIdAsync(dto.ProductId);
            if (product == null)
                throw new NotFoundException("Product not found");
            if (dto.Quantity <= 0)
                throw new BadRequestException("Invalid quantity");
            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == dto.ProductId);
            if (item != null)
            {
                if (dto.Quantity + item.Quantity > product.Stock)
                    throw new BadRequestException("Not enough stock");
                item.Quantity += dto.Quantity;
            }
            else
            {
                if (dto.Quantity > product.Stock)
                    throw new BadRequestException("Not enough stock");
                cart.CartItems.Add(new CartItem()
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    UnitPrice = product.Price
                });
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CartDto> GetCartAsync()
        {
            var cart = await GetCartOrThrow();
            var dto = _mapper.Map<CartDto>(cart);
            dto.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
            return dto;

        }

        public async Task RemoveItemAsync(int productId)
        {
            var cart = await GetCartOrThrow();
            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (item == null)
                throw new NotFoundException("Item not found");
            cart.CartItems.Remove(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ClearCartAsync()
        {
            var cart = await GetCartOrThrow();
            cart.CartItems.Clear();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
