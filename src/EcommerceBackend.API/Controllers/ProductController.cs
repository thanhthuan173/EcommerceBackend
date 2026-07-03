using AutoMapper;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Dto.Product;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Application.Interfaces.Services;
using EcommerceBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceBackend.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductQueryParameters query)
        {
            var result = await _service.GetAllAsync(query);
            return Ok(new
            {
                result.Items,
                result.PageNumber,
                result.PageSize,
                result.TotalCount,
                result.TotalPages
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            var product = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),new {id=product.Id},product);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute]int id, [FromBody]UpdateProductDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok("Update successfully");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoduct(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
