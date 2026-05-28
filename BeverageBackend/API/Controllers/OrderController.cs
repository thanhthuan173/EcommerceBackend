using AutoMapper;
using BeverageBackend.Application.Common.Query;
using BeverageBackend.Application.Dto;
using BeverageBackend.Application.Dto.Order;
using BeverageBackend.Application.Interfaces;
using BeverageBackend.Application.Interfaces.Services;
using BeverageBackend.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace BeverageBackend.API.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class OrderController:ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [Authorize(Roles ="ADMIN")]
        [HttpGet("all")]
        public async Task<IActionResult> GetOrders([FromQuery] OrderQueryParameters query)
        {
            var result = await _service.GetAllOrdersAsync(query);
            return Ok(new
            {
                result.Items,
                result.PageNumber,
                result.PageSize,
                result.TotalCount,
                result.TotalPages
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetMyOrders([FromQuery] OrderQueryParameters query)
        {
            var result = await _service.GetMyOrdersAsync(query);
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
        public async Task<IActionResult> GetOrderById(int id)
        {
            return Ok(await _service.GetOrderByIdAsync(id));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("admin/{id}")]
        public async Task<IActionResult> GetOrderByIdForAdmin(int id)
        {
            return Ok(await _service.GetOrderByIdForAdminAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var order = await _service.CreateOrderAsync();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] OrderStatus status)
        {
            await _service.UpdateStatusAsync(id, status);
            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            await _service.CancelOrderAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
