using AutoMapper;
using EcommerceBackend.Application.Dto;
using EcommerceBackend.Application.Dto.Cart;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Application.Interfaces.Services;
using EcommerceBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceBackend.API.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class CartController:ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            return Ok(await _service.GetCartAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddCartItemDto dto)
        {
            await _service.AddToCartAsync(dto);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            await _service.RemoveItemAsync(productId);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            await _service.ClearCartAsync();
            return NoContent();
        }
    }
}
