using BeverageBackend.Interfaces;
using BeverageBackend.Models;

namespace BeverageBackend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private BeverageDbContext _context;

        public CategoryRepository(BeverageDbContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(int id)
        {
            _context.Categories.Remove(GetCategory(id));
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public Category GetCategoryByProduct(int prodId)
        {
            return _context.Products.Where(p => p.Id == prodId).Select(p => p.Category).FirstOrDefault();
        }

        public ICollection<Product> GetProductsByCategory(int id)
        {
            var p = _context.Products.Where(p => p.CategoryId == id).ToList();
            return p;
        }

        public bool IsRemovable(int id)
        {
            return _context.Products.Any(p => p.CategoryId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
