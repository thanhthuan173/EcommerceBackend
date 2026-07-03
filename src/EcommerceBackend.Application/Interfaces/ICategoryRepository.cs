using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<PagedResult<Category>> GetAllAsync(CategoryQueryParameters query);
        Task<Category?> GetByIdAsync(int id);
        Task<Category?> GetByNameAsync(string name);

        Task<Category?> GetWithProductsAsync(int id);

        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);

        Task<bool> ExistsAsync(int id);
    }
}
