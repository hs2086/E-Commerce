using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager services;

        public ProductController(IServiceManager serviceManager)
        {
            this.services = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductParameters productParameters)
        {
            var pagedResult = await services.ProductService.GetAllProductsAsync(productParameters, false);

            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await services.ProductService.GetProductByIdAsync(id, false);
            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string query)
        {
            var products = await services.ProductService.SearchProductsAsync(query, false);
            return Ok(products);
        }

    }
}
