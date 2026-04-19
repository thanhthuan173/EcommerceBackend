using BeverageBackend.Models;

namespace BeverageBackend.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User? GetUser(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameOrEmailAsync(string account);
        ICollection<Order> GetOrdersByUser(int id);
        void Add(User user);
        bool CreateUser(User user);
        bool DeleteUser(int id);
        bool Save();
        bool UserExits(int id);
    }
}
