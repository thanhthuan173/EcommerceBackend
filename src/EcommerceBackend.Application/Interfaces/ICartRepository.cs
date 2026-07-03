using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetByUserWithItemsAsync(int userId);
        void Add(Cart cart);
    }
}
