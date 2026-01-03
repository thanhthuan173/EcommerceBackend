using BeverageBackend.Interfaces;
using BeverageBackend.Models;

namespace BeverageBackend.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private BeverageDbContext _context;
        private readonly ICartRepository _cart;

        public CustomerRepository(BeverageDbContext context,ICartRepository cart)
        {
            _context = context;
            _cart = cart;
        }

        public bool CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            _cart.CreateCart(customer);
            return Save();
        }

        public bool CustomerExits(int id)
        {
            return _context.Customers.Any(c => c.Id == id);
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Customer> GetCustomers()
        {
            return _context.Customers.OrderBy(c=>c.Id).ToList();
        }

        public ICollection<Order> GetOrdersByCustomer(int id)
        {
            var qr = _context.Orders.Where(o => o.CustomerId == id);
            return qr.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 1 ? true : false;
        }

        public bool DeleteCustomer(int id)
        {
            var cus = _context.Customers.Where(c => c.Id == id).FirstOrDefault();
            _context.Customers.Remove(cus);
            _cart.DeleteCart(id);
            return Save();
        }
    }
}
