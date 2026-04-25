using BeverageBackend.Application.Dto.Cart;
using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Interfaces
{
    public interface ICartRepository
    {
        ICollection<Cart> GetCarts();
        Cart GetCart(int id);
        User GetUserByCartId(int cartId);
        ICollection<CartItem> GetCartItems(int cartId);
        void CreateCart(Cart cart);
        void DeleteCart(int userId);
        bool AddCartItem(CartItem cartItem);
        bool DeleteCartItem(int cartId,int prodId);
        bool DeleteCartItems(int cartId);
        CartTotalDto TotalAmount(int cartId);
        bool Save();
        bool CartExists(int id);
    }
}
