using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BeverageBackend.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class OrderController:ControllerBase
    {
        private readonly IOrderRepository _order;
        private readonly ICartRepository _cart;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository order,ICartRepository cart,IMapper mapper)
        {
            _order = order;
            _cart = cart;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var oders = _mapper.Map<List<OrderDto>>(_order.GetOrders());
            return Ok(oders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOder(int id)
        {
            if (!_order.OrderExists(id))
                NotFound();
            var order = _mapper.Map<OrderDto>(_order.GetOrder(id));
            return Ok(order);
        }

        [HttpGet("{orderId}/user")]
        public IActionResult GetUserByOrderId(int orderId)
        {
            if (!_order.OrderExists(orderId))
                NotFound();
            var cus = _mapper.Map<UserDto>(_order.GetUserByOrderId(orderId));
            return Ok(cus);
        }

        [HttpPost("{cartId}")]
        public IActionResult CreateOrder(int cartId)
        {
            if (!_cart.CartExists(cartId))
                NotFound();
            if (_cart.GetCartItems(cartId).IsNullOrEmpty())
                return BadRequest("Cart is empty");
            var save = _order.CreateOrder(cartId);
            if (save==0)
            {
                ModelState.AddModelError("", "Error while saving");
                return BadRequest();
            }
            return Ok(save);
        }
    }
}
