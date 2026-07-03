using AutoMapper;
using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Dto.Product;
using EcommerceBackend.Application.Exceptions;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Application.Interfaces.Services;
using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _cateRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, ICategoryRepository cateRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _cateRepo = cateRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDto>> GetAllAsync(ProductQueryParameters query)
        {
            var result = await _repo.GetAllAsync(query);
            return new PagedResult<ProductDto>
                (
                    _mapper.Map<List<ProductDto>>(result.Items),
                    result.TotalCount,
                    result.PageNumber,
                    result.PageSize
                );
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
            _mapper.Map(dto, product);
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
