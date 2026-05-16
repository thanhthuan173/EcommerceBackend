using BeverageBackend.Application.Dto.Cart;
using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetByUserWithItemsAsync(int userId);
        void Add(Cart cart);
    }
}
