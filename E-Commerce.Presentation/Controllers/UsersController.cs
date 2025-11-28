using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObject.User;
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
    [Route("api/Users")]
    public class UsersController : ControllerBase 
    {
        private readonly IServiceManager serviceManager;
         
        public UsersController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userProfile = await serviceManager.UserService.GetUserProfileAsync(userId);
            return Ok(userProfile);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get([FromQuery] UserParameters userParameters)
        {
            var pagedResult = await serviceManager.UserService.GetAllUsersAsync(userParameters);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.users);
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUser([FromBody] UserRegisterDto model, [FromQuery] string role)
        {
            await serviceManager.UserService.AddUserAsync(model, role);
            return Ok("User added successfully.");
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser([FromQuery] string email)
        {
            await serviceManager.UserService.DeleteUserAsync(email);
            return Ok("User deleted successfully.");
        }
    }
}
