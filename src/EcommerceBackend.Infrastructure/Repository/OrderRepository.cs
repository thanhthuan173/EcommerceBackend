using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Domain.Models;
using EcommerceBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _context;

        public OrderRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        private async Task<PagedResult<Order>> ApplyQueryOptions(IQueryable<Order> orders, OrderQueryParameters query)
        {
            if (query.MinTotalAmount != null)
                orders = orders.Where(o => o.TotalAmount >= query.MinTotalAmount);
            if (query.MaxTotalAmount != null)
                orders = orders.Where(o => o.TotalAmount <= query.MaxTotalAmount);
            if (query.OrderStatus.HasValue)
                orders = orders.Where(o => o.Status == query.OrderStatus);
            if (query.IsDeleted != null)
                orders = orders.Where(o => o.IsDeleted == query.IsDeleted);

            switch (query.SortBy?.ToLower())
            {
                case "createddate":
                    orders = query.Desc ? orders.OrderByDescending(o => o.CreatedDate) : orders.OrderBy(o => o.CreatedDate);
                    break;
                case "totalamount":
                    orders = query.Desc ? orders.OrderByDescending(o => o.TotalAmount) : orders.OrderBy(o => o.TotalAmount);
                    break;
                default:
                    orders = orders.OrderByDescending(o => o.CreatedDate);
                    break;
            }

            var totalCount = await orders.CountAsync();
            var items = await orders.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
            return new PagedResult<Order>(items, totalCount, query.PageNumber, query.PageSize);
        }

        public async Task<PagedResult<Order>> GetAllOrdersAsync(OrderQueryParameters query)
        {
            var orders = _context.Orders.AsQueryable();
            return await ApplyQueryOptions(orders, query);
        }

        public async Task<PagedResult<Order>> GetByUserAsync(int userId, OrderQueryParameters query)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId && !o.IsDeleted);
            query.IsDeleted = null;
            return await ApplyQueryOptions(orders, query);
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
