using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObject.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/Carts")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public CartController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCart([FromBody] AddCartItemDto addCartItem)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var cart = await serviceManager.CartService.AddToCartAsync(addCartItem, userId);
            return Ok(cart);
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var cart = await serviceManager.CartService.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartItemDto updateCartItem)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var cart = await serviceManager.CartService.UpdateCartAsync(updateCartItem, userId);
            return Ok(cart);
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveCartItem(int productId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var cart = await serviceManager.CartService.RemoveCartItemAsync(userId, productId);
            return Ok(cart);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            await serviceManager.CartService.ClearCartAsync(userId);
            return NoContent();
        }
    }
}
