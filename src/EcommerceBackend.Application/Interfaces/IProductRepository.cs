using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<PagedResult<Product>> GetAllAsync(ProductQueryParameters query);
        Task<Product?> GetByIdAsync(int id);

        Task<Product?> IsNameExistsAsync(string prodName, int cateId);

        void Add(Product product);
        void Delete(Product product);

        Task<bool> ExistsAsync(int id);
    }
}
