using AutoMapper;
using BeverageBackend.Application.Dto;
using BeverageBackend.Application.Interfaces;
using BeverageBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BeverageBackend.API.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(Roles ="ADMIN")]
    public class CategoryController:ControllerBase
    {
        private ICategoryRepository _category;
        private IMapper _mapper;

        public CategoryController(ICategoryRepository category,IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var cates = _mapper.Map<List<CategoryDto>>(_category.GetCategories());
            return StatusCode(200, cates);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            if (!_category.CategoryExists(id))
                return StatusCode(404,"Khong tim thay category");
            var cate = _mapper.Map<CategoryDto>(_category.GetCategory(id));
            return StatusCode(200, cate);
        }

        [HttpGet("{id}/products")]
        public IActionResult GetAllProductByCateId(int id)
        {
            if (!_category.CategoryExists(id))
                return NotFound();
            var prods = _mapper.Map<List<ProductDto>>(_category.GetProductsByCategory(id));
            return Ok(prods);
        }

        [HttpGet("{prodId}/category")]
        public IActionResult GetCategoryByProductId(int prodId)
        {
            return Ok(_category.GetCategoryByProduct(prodId));
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var category = _category.GetCategories().Where(c => c.Name.Trim().ToUpper() == categoryDto.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (category != null)
            {
                ModelState.AddModelError("Name", "Category already exits");
                return BadRequest(ModelState);
            }
            var categoryMap = _mapper.Map<Category>(categoryDto);
            if (!_category.CreateCategory(categoryMap))
            {
                return StatusCode(500, "Error while saving");
            }
            return Ok("Create Successfully");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute]int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var category = _category.GetCategory(id);
            if (category == null)
                return NotFound();
            var newName = updateCategoryDto.Name.Trim();
            if (category.Name == newName)
                return NoContent();
            category.Name = newName;
            if (!_category.UpdateCategory(category))
            {
                ModelState.AddModelError("","Error while saving");
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (!_category.CategoryExists(id))
                return NotFound();
            if (_category.IsRemovable(id))
            {
                ModelState.AddModelError("Category", "Can't delete this category because it has associated products");
                return BadRequest(ModelState);
            }
            if (!_category.DeleteCategory(id))
            {
                return StatusCode(500, "Error while saving");
            }
            return Ok("Delete successfully");
        }
    }
}
