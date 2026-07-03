using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<PagedResult<Order>> GetAllOrdersAsync(OrderQueryParameters query);
        Task<PagedResult<Order>> GetByUserAsync(int userId, OrderQueryParameters query);
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> GetByIdWithItemsAsync(int id, bool includeDeleted = false);
        void Add(Order order);
    }
}
