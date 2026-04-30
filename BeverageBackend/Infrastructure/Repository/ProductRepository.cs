using BeverageBackend.Application.Interfaces;
using BeverageBackend.Domain.Models;
using BeverageBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeverageBackend.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly BeverageDbContext _context;
        public ProductRepository(BeverageDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p=>p.Category).ToListAsync();
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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
