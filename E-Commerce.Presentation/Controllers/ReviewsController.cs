using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObject.Review;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ReviewsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery] ReviewParameters reviewParameters)
        {
            var pagedResult = await serviceManager.ReviewService.GetAllReviewsAsync(reviewParameters);

            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.reviews);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto reviewCreateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var reviewDto = await serviceManager.ReviewService.AddReviewAsync(reviewCreateDto, userId);
            return CreatedAtAction(nameof(GetReviewById), new { id = reviewDto.Id }, reviewDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var reviewDto = await serviceManager.ReviewService.GetReviewByIdAsync(id);
            return Ok(reviewDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewDto updateReview)
        {
            await serviceManager.ReviewService.UpdateReviewAsync(id, updateReview);
            return Ok("Updated Successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await serviceManager.ReviewService.DeleteReviewAsync(id);
            return Ok("Deleted Successfully.");
        }
    }
}
