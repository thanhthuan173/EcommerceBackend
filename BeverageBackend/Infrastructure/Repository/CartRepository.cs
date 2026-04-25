using BeverageBackend.Application.Dto.Cart;
using BeverageBackend.Application.Interfaces;
using BeverageBackend.Domain.Models;
using BeverageBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeverageBackend.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly BeverageDbContext _context;

        public CartRepository(BeverageDbContext context)
        {
            _context = context;
        }

        public bool CartExists(int id)
        {
            return _context.Carts.Any(c => c.Id == id);
        }

        public Cart GetCart(int id)
        {
            return _context.Carts.Where(c => c.Id == id).Include(c=>c.CartItems).ThenInclude(ci=>ci.Product).FirstOrDefault();
        }

        public ICollection<Cart> GetCarts()
        {
            return _context.Carts.ToList();
        }

        public User GetUserByCartId(int cartId)
        {
            return _context.Carts.Where(c => c.Id == cartId).Select(c => c.User).FirstOrDefault();
        }

        public ICollection<CartItem> GetCartItems(int cartId)
        {
            return _context.CartItems.Where(ci => ci.CartId == cartId).Include(ci=>ci.Product).ToList();
        }

        public void CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public void DeleteCart(int userId)
        {
            var cart = _context.Carts.Where(c => c.UserId == userId).FirstOrDefault();
            _context.Carts.Remove(cart);
        }

        public bool AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public CartTotalDto TotalAmount(int cartId)
        {
            var total = _context.CartItems.Where(ci => ci.CartId == cartId).Sum(ci => ci.UnitPrice * ci.Quantity);
            var cartTotal = new CartTotalDto
            {
                CartId = cartId,
                TotalAmount = total
            };
            return cartTotal;
        }

        public bool DeleteCartItem(int cartId, int prodId)
        {
            var cartItem=  _context.CartItems.Where(ci => ci.CartId == cartId && ci.ProductId == prodId).FirstOrDefault();
            if (cartItem == null)
                return false;
            _context.CartItems.Remove(cartItem);
            return Save();
        }

        public bool DeleteCartItems(int cartId)
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == cartId).ToList();
            _context.CartItems.RemoveRange(cartItems);
            return Save();
        }
    }
}
