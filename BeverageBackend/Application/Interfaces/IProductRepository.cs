using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);

        Task<Product?> IsNameExistsAsync(string prodName, int cateId);

        void Add(Product product);
        void Delete(Product product);

        Task<bool> ExistsAsync(int id);
    }
}
