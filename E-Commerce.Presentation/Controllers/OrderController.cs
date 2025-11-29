using Entities.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObject.Checkout;
using Shared.DataTransferObject.Order;
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
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public OrderController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }


        [HttpGet("me")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orders = await serviceManager.OrderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpPost("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus status)
        {
            await serviceManager.OrderService.UpdateOrderStatusAsync(id, status);
            return NoContent();
        }

        [HttpPost("make")]
        public async Task<IActionResult> MakeOrder([FromBody] AddOrderDto order)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var checkout =  await serviceManager.OrderService.MakeOrderAsync(order, userId);
            return Ok(new { Order = checkout.checkout1, PaymentUrl = checkout.url });
        }
    }
}
