using BeverageBackend.Application.Common;
using BeverageBackend.Application.Common.Query;
using BeverageBackend.Application.Dto.Order;
using BeverageBackend.Domain.Enums;

namespace BeverageBackend.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<PagedResult<OrderDto>> GetAllOrdersAsync(OrderQueryParameters query);
        Task<PagedResult<OrderDto>> GetMyOrdersAsync(OrderQueryParameters query);
        Task<OrderDetailDto> GetOrderByIdAsync(int orderId);
        Task<OrderDetailDto> GetOrderByIdForAdminAsync(int orderId);
        Task<OrderDetailDto> CreateOrderAsync();
        Task UpdateStatusAsync(int orderId, OrderStatus status);
        Task CancelOrderAsync(int orderId);
        Task DeleteOrderAsync(int orderId);
    }
}
