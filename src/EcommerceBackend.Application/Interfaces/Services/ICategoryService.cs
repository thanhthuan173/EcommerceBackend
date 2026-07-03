using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Dto;
using EcommerceBackend.Application.Dto.Product;

namespace EcommerceBackend.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryDto>> GetAllAsync(CategoryQueryParameters query);
        Task<CategoryDto> GetByIdAsync(int id);

        Task<CategoryWithProductsDto> GetCategoryWithProductsAsync(int id);

        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(int id, UpdateCategoryDto dto);
        Task DeleteAsync(int id);
    }
}
