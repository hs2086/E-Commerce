using AutoMapper;
using Contracts.IRepository;
using Contracts.Logger;
using Services.Contracts;
using Shared.DataTransferObject.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public RoleService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task CreateRoleAsync(CreateRoleDto createRole)
        {
            await repositoryManager.Role.CreateRoleAsync(createRole.Name);
            await repositoryManager.SaveAsync();
        }

        public async Task DeleteRoleAsync(DeleteRoleDto deleteRole)
        {
            await repositoryManager.Role.DeleteRoleAsync(deleteRole.Name);
            await repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await repositoryManager.Role.GetAllRolesAsync();
            var rolesDto = mapper.Map<IEnumerable<RoleDto>>(roles);
            return rolesDto;
        }

        public async Task UpdateRoleAsync(RoleUpdateDto roleUpdate)
        {
            await repositoryManager.Role.UpdateRoleAsync(roleUpdate);
            await repositoryManager.SaveAsync();
        }

        public async Task AssignRoleToUserAsync(AssignRoleDto assignRole)
        {
            await repositoryManager.Auth.AssignRoleAsync(assignRole.Email, assignRole.RoleName);
            await repositoryManager.SaveAsync();
        }

        public async Task CheckRoleExistAsync(CheckRoleExistDto checkRoleExist)
        {
            await repositoryManager.Role.CheckIfRoleExistAsync(checkRoleExist.RoleName);
        }

        public async Task<IEnumerable<RoleDto>> GetRolesByUserAsync(GetRoleByUserDto getRoleByUser)
        {
            var roles = await repositoryManager.Auth.GetRolesAsync(getRoleByUser.Email);
            return mapper.Map<IEnumerable<RoleDto>>(roles);
        }
    }
}
