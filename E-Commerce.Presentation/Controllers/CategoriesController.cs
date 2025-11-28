using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObject.Category;
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

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto category)
        {
            var createdCategory = await service.CategoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto categoryDto)
        {
            await service.CategoryService.UpdateCategoryAsync(id, categoryDto, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await service.CategoryService.DeleteCategoryAsync(id, trackChanges: false);
            return NoContent();
        }

    }
}
