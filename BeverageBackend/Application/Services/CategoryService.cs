using AutoMapper;
using BeverageBackend.Application.Dto;
using BeverageBackend.Application.Dto.Product;
using BeverageBackend.Application.Exceptions;
using BeverageBackend.Application.Interfaces;
using BeverageBackend.Application.Interfaces.Services;
using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = _mapper.Map<IEnumerable<CategoryDto>>(await _repo.GetAllAsync());
            return categories;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException("Category not found");
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryWithProductsDto> GetCategoryWithProductsAsync(int id)
        {
            var category = await _repo.GetWithProductsAsync(id);
            if (category == null)
                throw new NotFoundException("Category not found");
            return _mapper.Map<CategoryWithProductsDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var normalizedName = dto.Name.Trim().ToUpper();
            var category = await _repo.GetByNameAsync(normalizedName);
            if (category != null)
                throw new AlreadyExistsException("Category already existed");
            var entity = _mapper.Map<Category>(dto);
            entity.Name = normalizedName;
            _repo.Add(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var normalizedName = dto.Name.Trim().ToUpper();
            var category = await _repo.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException("Category not found");
            var cateName = await _repo.GetByNameAsync(normalizedName);
            if (cateName != null && cateName.Id != id)
                throw new AlreadyExistsException("Category already existed");
            category.Name = normalizedName;
            _repo.Update(category);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repo.GetWithProductsAsync(id);
            if (category == null)
                throw new NotFoundException("Category not found");
            if (category.Products.Any())
                throw new HasAssociatedException("Category is in use");
            _repo.Delete(category);
            await _repo.SaveChangesAsync();
        }
    }
}
