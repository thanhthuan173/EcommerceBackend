using EcommerceBackend.Application.Dto.Cart;

namespace EcommerceBackend.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync();
        Task AddToCartAsync(AddCartItemDto dto);
        Task RemoveItemAsync(int productId);
        Task ClearCartAsync();
    }
}
