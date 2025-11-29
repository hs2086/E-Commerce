using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/wishlist")]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public WishlistController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddToWishlist(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await serviceManager.WishlistService.AddToWishlist(userId, id);

            return Ok("Added Successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromWishlist(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await serviceManager.WishlistService.DeleteFromWishList(userId, id);

            return Ok("Deleted Successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var wishlistItems = await serviceManager.WishlistService.GetAllWishlistItems(userId);
            return Ok(wishlistItems);
        }

        [HttpGet("is-exists/{productId}")]
        public async Task<IActionResult> CheckIfExists(int productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await serviceManager.WishlistService.CheckIfProductIsExistsInWishlist(userId, productId);
            return Ok("Found.");
        }

    }
}
