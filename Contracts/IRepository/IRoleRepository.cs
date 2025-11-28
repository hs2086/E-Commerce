using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObject.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IRoleRepository
    {
        Task CreateRoleAsync(string roleName);
        Task DeleteRoleAsync(string roleName);
        Task<IEnumerable<IdentityRole>> GetAllRolesAsync();
        Task UpdateRoleAsync(RoleUpdateDto roleUpdate);
        Task CheckIfRoleExistAsync(string roleName);
    }
}
