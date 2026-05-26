using BeverageBackend.Application.Interfaces;
using BeverageBackend.Domain.Models;
using BeverageBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeverageBackend.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BeverageDbContext _context;

        public OrderRepository(BeverageDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByUserAsync(int userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId && !o.IsDeleted).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> GetByIdWithItemsAsync(int id, bool includeDeleted = false)
        {
            return await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == id && (includeDeleted || !o.IsDeleted));
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }
    }
}
