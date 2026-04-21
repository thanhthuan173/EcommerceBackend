using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Dto.Cart;
using BeverageBackend.Interfaces;
using BeverageBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeverageBackend.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class CartController:ControllerBase
    {
        private readonly ICartRepository _cart;
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cart,IProductRepository product,IMapper mapper)
        {
            _cart = cart;
            _product = product;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCarts()
        {
            var carts = _mapper.Map<List<CartDto>>(_cart.GetCarts());
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public IActionResult GetCart(int id)
        {
            if (!_cart.CartExists(id))
                return NotFound();
            var cart = _mapper.Map<CartDto>(_cart.GetCart(id));
            return Ok(cart);
        }

        [HttpGet("{cartId}/user")]
        public IActionResult GetUserByCartId(int cartId)
        {
            if (!_cart.CartExists(cartId))
                return NotFound();
            var cus = _mapper.Map<UserDto>(_cart.GetUserByCartId(cartId));
            return Ok(cus);
        }

        [HttpPost]
        public IActionResult AddItemToCart([FromBody]AddCartItemDto cartItemDto)
        {
            var cartItems = _cart.GetCartItems(cartItemDto.CartId);
            var cartItem = cartItems.Where(ci => ci.ProductId == cartItemDto.ProductId).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Quantity += cartItemDto.Quantity;
                if (!_cart.Save())
                {
                    ModelState.AddModelError("", "Error while saving");
                    return BadRequest(ModelState);
                }
                return NoContent();
            }
            var price = _product.GetProduct(cartItemDto.ProductId).Price;
            var mapCartItem = _mapper.Map<CartItem>(cartItemDto);
            mapCartItem.UnitPrice = price;
            if (!_cart.AddCartItem(mapCartItem))
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        [HttpGet("{cartId}/items")]
        public IActionResult GetCartItems(int cartId)
        {
            if (!_cart.CartExists(cartId))
                return NotFound();
            var prods = _mapper.Map<List<CartItemDto>>(_cart.GetCartItems(cartId));
            return Ok(prods);
        }

        [HttpGet("{cartId}/total")]
        public IActionResult GetTotalAmount([FromRoute]int cartId)
        {
            if (!_cart.CartExists(cartId))
                return NotFound();
            var total = _cart.TotalAmount(cartId);
            return Ok(total);
        }

        [HttpDelete("{cartId}/items/{prodId}")]
        public IActionResult DeleteCartItem([FromRoute]int cartId, [FromRoute] int prodId)
        {
            var isDeleted = _cart.DeleteCartItem(cartId, prodId);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{cartId}/items")]
        public IActionResult DeleteCartItems([FromRoute]int cartId)
        {
            if (!_cart.DeleteCartItems(cartId))
            {
                ModelState.AddModelError("", "Error while saving");
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
