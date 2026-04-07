using BeverageBackend.Interfaces;
using BeverageBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageBackend.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BeverageDbContext _context;
        private readonly ICartRepository _cartRepository;

        public OrderRepository(BeverageDbContext context,ICartRepository cartRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
        }

        public int CreateOrder(int cartId)
        {
            var cart = _context.Carts.Where(c => c.Id == cartId).FirstOrDefault();
            var order = new Order()
            {
                TotalAmount = _cartRepository.TotalAmount(cartId).TotalAmount,
                UserId = cart.UserId,
            };
            var items = _cartRepository.GetCartItems(cartId);
            var orderItems = new List<OrderItem>();
            foreach (var item in items)
            {
                orderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }
            order.OrderItems = orderItems;
            _context.Orders.Add(order);
            var save = _context.SaveChanges();
            if (save == 0)
                return save;
            //_cartRepository.DeleteCartItems(cartId);
            return order.Id;
        }

        public User GetUserByOrderId(int orderId)
        {
            return _context.Orders.Where(o => o.Id == orderId).Select(o => o.User).FirstOrDefault();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Where(o => o.Id == id).Include(o => o.OrderItems).ThenInclude(oi=>oi.Product).FirstOrDefault();
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(o => o.Id == orderId);
        }
    }
}
