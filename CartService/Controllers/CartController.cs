using CartService.Models;
using CartService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [ApiController]

    [Route("api/cart")]

    public class CartController : ControllerBase

    {

        private readonly CartRedisService _cartService;
        public CartController(CartRedisService cartService)
        {

            _cartService = cartService;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToCart(string userId, CartItem item)
        {
            await _cartService.AddToCartAsync(userId, item);
            return Ok("Item added to cart");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _cartService.GetCart(userId);
            return Ok(cart);
        }
    }

}