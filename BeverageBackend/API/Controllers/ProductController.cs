using AutoMapper;
using BeverageBackend.Application.Dto.Product;
using BeverageBackend.Application.Interfaces;
using BeverageBackend.Application.Interfaces.Services;
using BeverageBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BeverageBackend.API.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            var product = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),new {id=product.Id},product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute]int id, [FromBody]UpdateProductDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok("Update successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoduct(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
