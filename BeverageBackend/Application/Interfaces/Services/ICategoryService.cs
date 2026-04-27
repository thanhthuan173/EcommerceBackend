using BeverageBackend.Application.Dto;
using BeverageBackend.Application.Dto.Product;

namespace BeverageBackend.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);

        Task<CategoryWithProductsDto> GetCategoryWithProductsAsync(int id);

        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(int id, UpdateCategoryDto dto);
        Task DeleteAsync(int id);
    }
}
