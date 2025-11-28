using Shared.DataTransferObject.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRoleService
    {
        Task CreateRoleAsync(CreateRoleDto createRole);
        Task DeleteRoleAsync(DeleteRoleDto deleteRole);
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task UpdateRoleAsync(RoleUpdateDto roleUpdate);
        Task AssignRoleToUserAsync(AssignRoleDto assignRole);
        Task CheckRoleExistAsync(CheckRoleExistDto checkRoleExist);
        Task<IEnumerable<RoleDto>> GetRolesByUserAsync(GetRoleByUserDto getRoleByUser);
    }
}
