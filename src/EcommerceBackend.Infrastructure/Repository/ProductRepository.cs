using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Domain.Models;
using EcommerceBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;
        public ProductRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Product>> GetAllAsync(ProductQueryParameters query)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(query.Name))
            {
                products = products.Where(p => p.Name.ToLower().Contains(query.Name.ToLower()));
            }
            if (query.MinValue != null)
            {
                products = products.Where(p => p.Price >= query.MinValue);
            }
            if (query.MaxValue != null)
            {
                products = products.Where(p => p.Price <= query.MaxValue);
            }
            if (query.CategoryId != null)
            {
                products = products.Where(p => p.CategoryId == query.CategoryId);
            }

            switch (query.SortBy?.ToLower())
            {
                case "price":
                    products = query.Desc ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
                    break;
                case "name":
                    products = query.Desc ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name);
                    break;
            }

            var totalCount = await products.CountAsync();
            var items = await products
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Include(p=>p.Category)
                .ToListAsync();
            return new PagedResult<Product>(items, totalCount, query.PageNumber, query.PageSize);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p=>p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> IsNameExistsAsync(string prodName, int cateId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name.ToLower() == prodName.ToLower() && p.CategoryId == cateId);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
    }
}
