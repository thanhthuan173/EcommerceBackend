using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Interfaces;
using BeverageBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeverageBackend.Controllers
{ 
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly IProductRepository _product;
        private readonly ICategoryRepository _category;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository,IMapper mapper,ICategoryRepository category)
        {
            _product = productRepository;
            _category = category;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var prod = _mapper.Map<List<ProductDto>>(_product.GetProducts().ToList());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(prod);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            if (!_product.ProductExists(id))
                return NotFound();
            var prod = _mapper.Map<ProductDto>(_product.GetProduct(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(prod);
        }

        [HttpGet("{name}")]
        public IActionResult GetProductByName(string name)
        {
            var prod = _mapper.Map<ProductDto>(_product.GetProduct(name));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(prod);
        }

        [HttpGet("{prodId}/count_orders")]
        public IActionResult ProductOrders(int prodId)
        {
            if (!_product.ProductExists(prodId))
                return NotFound();
            return Ok(_product.CountOrders(prodId));
        }

        [HttpGet("{prodId}/count_carts")]
        public IActionResult ProductCarts(int prodId)
        {
            if (!_product.ProductExists(prodId))
                return NotFound();
            return Ok(_product.CountCarts(prodId));
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {
            var product = _product.GetProducts().Where(p => p.Name == productDto.Name&&p.CategoryId==productDto.CategoryId).FirstOrDefault();
            if (product != null)
            {
                ModelState.AddModelError("Name","Product already exist");
                return BadRequest(ModelState);
            }
            if (!_category.CategoryExists(productDto.CategoryId))
            {
                return NotFound();
            }
            var prodMap = _mapper.Map<Product>(productDto);
            if (!_product.CreateProduct(prodMap))
            {
                return StatusCode(500, "Error while saving");
            }
            return Ok("Create successfully");
        }
    }
}
