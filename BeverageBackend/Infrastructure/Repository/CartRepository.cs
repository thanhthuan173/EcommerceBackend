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

        public void Add(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public async Task<Cart?> GetByUserWithItemsAsync(int userId)
        {
            return await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
