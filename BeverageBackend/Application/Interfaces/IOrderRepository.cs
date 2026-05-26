using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetByUserAsync(int userId);
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> GetByIdWithItemsAsync(int id, bool includeDeleted = false);
        void Add(Order order);
    }
}
