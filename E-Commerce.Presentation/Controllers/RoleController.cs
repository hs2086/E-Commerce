using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObject.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager service;

        public RoleController(IServiceManager service)
        {
            this.service = service;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRole)
        {
            await service.RoleService.CreateRoleAsync(createRole);
            return Ok($"Role '{createRole.Name}' created successfully.");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleDto deleteRole)
        {
            await service.RoleService.DeleteRoleAsync(deleteRole);
            return Ok($"Role '{deleteRole.Name}' deleted successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await service.RoleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateDto roleUpdate)
        {
            await service.RoleService.UpdateRoleAsync(roleUpdate);
            return Ok($"Role '{roleUpdate.OldName}' updated to '{roleUpdate.NewName}' successfully.");
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignRole)
        {
            await service.RoleService.AssignRoleToUserAsync(assignRole);
            return Ok($"Role '{assignRole.RoleName}' assigned to user '{assignRole.Email}' successfully.");
        }

        [HttpPost("exist")]
        public async Task<IActionResult> CheckRoleExist([FromBody] CheckRoleExistDto createRole)
        {
            await service.RoleService.CheckRoleExistAsync(createRole);
            return Ok($"{createRole.RoleName} is Exist.");
        }

        [HttpGet("get-roles-by-user")]
        public async Task<IActionResult> GetRolesByUser([FromQuery] GetRoleByUserDto getRoleByUser)
        {
            var roles = await service.RoleService.GetRolesByUserAsync(getRoleByUser);
            return Ok(roles);
        }
    }
}
