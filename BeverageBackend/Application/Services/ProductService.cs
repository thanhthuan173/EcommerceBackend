using AutoMapper;
using BeverageBackend.Application.Dto.Product;
using BeverageBackend.Application.Exceptions;
using BeverageBackend.Application.Interfaces;
using BeverageBackend.Application.Interfaces.Services;
using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _cateRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo,ICategoryRepository cateRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _cateRepo = cateRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = _mapper.Map<IEnumerable<ProductDto>>(await _repo.GetAllAsync());
            return products;
        }

        public async Task<ProductDto> GetById(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("Product not found");
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            if (await _repo.IsNameExistsAsync(dto.Name, dto.CategoryId) != null)
                throw new AlreadyExistsException("Product name already exists in this category");
            if (!await _cateRepo.ExistsAsync(dto.CategoryId))
                throw new NotFoundException("Category not found");
            var entity = _mapper.Map<Product>(dto);
            _repo.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("Product not found");
            if (!await _cateRepo.ExistsAsync(dto.CategoryId))
                throw new NotFoundException("Category not found");
            var isExisted = await _repo.IsNameExistsAsync(dto.Name, dto.CategoryId);
            if (isExisted != null && isExisted.Id != id)
                throw new AlreadyExistsException("Product name already exists in this category");
            _mapper.Map(dto,product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("Product not found");
            _repo.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
