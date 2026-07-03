using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Dto.Order;
using EcommerceBackend.Domain.Enums;

namespace EcommerceBackend.Application.Interfaces.Services
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
