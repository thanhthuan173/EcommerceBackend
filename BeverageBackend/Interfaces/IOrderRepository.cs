using BeverageBackend.Models;

namespace BeverageBackend.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int id);
        Customer GetCustomerByOrderId(int orderId);
        int CreateOrder(int cartId);
        bool OrderExists(int orderId);
    }
}
