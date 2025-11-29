using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObject.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public PaymentsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost("callback")]
        public async Task<IActionResult> PaymentCallback([FromBody] PaymobCallbackDto dto)
        {
            await serviceManager.PaymentService.HandlePaymobCallbackAsync(dto);
            return Ok();
        }
    }

}
