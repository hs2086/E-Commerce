using Contracts.IRepository;
using Entities.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObject.Role;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task CreateRoleAsync(string roleName)
        {
            bool roleExist = await roleManager.RoleExistsAsync(roleName.ToLower());
            if (roleExist)
            {
                throw new RoleAlreadyExistBadRequestException($"Role '{roleName}' already exists.");
            }
            IdentityRole role = new IdentityRole
            {
                Name = roleName,
            };
            await roleManager.CreateAsync(role);
        }

        public async Task DeleteRoleAsync(string roleName)
        {
            IdentityRole? role = await roleManager.FindByNameAsync(roleName.ToLower());
            if (role == null)
            {
                throw new RoleNotFoundException($"Role '{roleName}' not found.");
            }
            await roleManager.DeleteAsync(role);
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task UpdateRoleAsync(RoleUpdateDto roleUpdate)
        {
            IdentityRole roleDb = await roleManager.FindByNameAsync(roleUpdate.OldName.ToLower())
                ?? throw new RoleNotFoundException($"Role '{roleUpdate.OldName}' not found.");

            roleDb.Name = roleUpdate.NewName.ToLower();
            await roleManager.UpdateAsync(roleDb);
        }

        public async Task CheckIfRoleExistAsync(string roleName)
        {
            bool roleExist = await roleManager.RoleExistsAsync(roleName.ToLower());
            if (!roleExist)
            {
                throw new RoleNotFoundException($"Role '{roleName}' not found.");
            }
        }
    }
}
