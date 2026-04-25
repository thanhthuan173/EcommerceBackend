using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int id);
        User GetUserByOrderId(int orderId);
        int CreateOrder(int cartId);
        bool OrderExists(int orderId);
    }
}
