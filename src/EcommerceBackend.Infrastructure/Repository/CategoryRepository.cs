using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Domain.Models;
using EcommerceBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceBackend.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private EcommerceDbContext _context;

        public CategoryRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Category>> GetAllAsync(CategoryQueryParameters query)
        {
            var categories = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(query.Name))
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(query.Name.ToLower()));
            }

            switch (query.SortBy?.ToLower())
            {
                case "name":
                    categories = query.Desc ? categories.OrderByDescending(c => c.Name) : categories.OrderBy(c => c.Name);
                    break;
                default:
                    categories = categories.OrderBy(c => c.Id);
                    break;
            }

            var totalCount = await categories.CountAsync();
            var items = await categories
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
            return new PagedResult<Category>(items, totalCount, query.PageNumber, query.PageSize);
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Category?> GetWithProductsAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
