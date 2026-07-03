using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Domain.Models;
using EcommerceBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly EcommerceDbContext _context;

        public CartRepository(EcommerceDbContext context)
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
