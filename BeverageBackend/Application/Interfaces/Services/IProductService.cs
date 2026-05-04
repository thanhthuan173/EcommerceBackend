using BeverageBackend.Application.Common;
using BeverageBackend.Application.Common.Query;
using BeverageBackend.Application.Dto.Product;

namespace BeverageBackend.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetAllAsync(ProductQueryParameters query);
        Task<ProductDto> GetById(int id);

        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task UpdateAsync(int id, UpdateProductDto dto);
        Task DeleteAsync(int id);
    }
}
