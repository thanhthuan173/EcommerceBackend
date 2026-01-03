using BeverageBackend.Models;

namespace BeverageBackend.Interfaces
{
    public interface ICartRepository
    {
        ICollection<Cart> GetCarts();
        Cart GetCart(int id);
        Customer GetCustomerByCartId(int cartId);
        ICollection<CartItem> GetCartItems(int cartId);
        void CreateCart(Customer customer);
        void DeleteCart(int customerId);
        bool CartExists(int id);
    }
}
