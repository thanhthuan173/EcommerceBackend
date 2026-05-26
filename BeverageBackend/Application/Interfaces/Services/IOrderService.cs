using BeverageBackend.Application.Dto.Order;
using BeverageBackend.Domain.Enums;

namespace BeverageBackend.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDto>> GetMyOrdersAsync();
        Task<OrderDetailDto> GetOrderByIdAsync(int orderId);
        Task<OrderDetailDto> GetOrderByIdForAdminAsync(int orderId);
        Task<OrderDetailDto> CreateOrderAsync();
        Task UpdateStatusAsync(int orderId, OrderStatus status);
        Task CancelOrderAsync(int orderId);
        Task DeleteOrderAsync(int orderId);
    }
}
