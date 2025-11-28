using Microsoft.AspNetCore.Authorization;
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
    [Route("api/categories")]
    public class CategoriesController : ControllerBase 
    {
        private readonly IServiceManager service;

        public CategoriesController(IServiceManager service)
        { 
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await service.CategoryService.GetAllCategoriesAsync(trackChanges: false);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await service.CategoryService.GetByIdAsync(id, false);
            return Ok(category);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductByCategoryId(int id, [FromQuery] ProductParameters productParameters)
        {
            var pagedResult = await service.CategoryService.GetProductsByCategoryIdAsync(id, productParameters, false);

            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.products);
        }

    }
}
