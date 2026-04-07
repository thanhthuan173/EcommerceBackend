using BeverageBackend.Interfaces;
using BeverageBackend.Models;

namespace BeverageBackend.Repository
{
    public class UserRepository : IUserRepository
    {
        private BeverageDbContext _context;
        private readonly ICartRepository _cart;

        public UserRepository(BeverageDbContext context,ICartRepository cart)
        {
            _context = context;
            _cart = cart;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            _cart.CreateCart(user);
            return Save();
        }

        public bool UserExits(int id)
        {
            return _context.Users.Any(c => c.Id == id);
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(c=>c.Id).ToList();
        }

        public ICollection<Order> GetOrdersByUser(int id)
        {
            var qr = _context.Orders.Where(o => o.UserId == id);
            return qr.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 1 ? true : false;
        }

        public bool DeleteUser(int id)
        {
            var cus = _context.Users.Where(c => c.Id == id).FirstOrDefault();
            _context.Users.Remove(cus);
            _cart.DeleteCart(id);
            return Save();
        }
    }
}
