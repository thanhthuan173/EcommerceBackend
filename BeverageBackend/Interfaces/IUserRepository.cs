using BeverageBackend.Models;

namespace BeverageBackend.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        ICollection<Order> GetOrdersByUser(int id);
        bool CreateUser(User user);
        bool DeleteUser(int id);
        bool Save();
        bool UserExits(int id);
    }
}
