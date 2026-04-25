using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Product> GetProductsByCategory(int id);
        Category GetCategoryByProduct(int prodId);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool Save();
        bool DeleteCategory(int id);
        bool IsRemovable(int id);
    }
}
