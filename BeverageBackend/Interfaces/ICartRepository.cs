using BeverageBackend.Dto.Cart;
using BeverageBackend.Models;

namespace BeverageBackend.Interfaces
{
    public interface ICartRepository
    {
        ICollection<Cart> GetCarts();
        Cart GetCart(int id);
        User GetUserByCartId(int cartId);
        ICollection<CartItem> GetCartItems(int cartId);
        void CreateCart(User user);
        void DeleteCart(int userId);
        bool AddCartItem(CartItem cartItem);
        bool DeleteCartItem(int cartId,int prodId);
        bool DeleteCartItems(int cartId);
        CartTotalDto TotalAmount(int cartId);
        bool Save();
        bool CartExists(int id);
    }
}
