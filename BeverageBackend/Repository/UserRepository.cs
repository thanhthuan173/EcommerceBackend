using BeverageBackend.Interfaces;
using BeverageBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageBackend.Repository
{
    public class UserRepository : IUserRepository
    {
        private BeverageDbContext _context;

        public UserRepository(BeverageDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public bool UserExits(int id)
        {
            return _context.Users.Any(c => c.Id == id);
        }

        public User? GetUser(int id)
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
            return saved > 0 ? true : false;
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.Where(c => c.Id == id).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            user.IsActive = false;
            return Save();
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }
    }
}
