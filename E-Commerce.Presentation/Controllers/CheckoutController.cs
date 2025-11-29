using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObject.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public CheckoutController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Process(AddCheckoutDto checkout)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var checkoutDto = await serviceManager.CheckoutService.ProcessCheckoutAsync(userId, checkout);
            return Ok(new { Order = checkoutDto.checkout1, PaymentUrl = checkoutDto.url });
        }
    }
}
