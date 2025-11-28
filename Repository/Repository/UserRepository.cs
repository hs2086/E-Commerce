using Contracts.IRepository;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObject.User;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return new UserDto
            {
                UserName = user?.UserName ?? "",
                Email = user?.Email ?? ""
            };
        }

        public async Task AddUserAsync(ApplicationUser user, string password)
        {
            await userManager.CreateAsync(user, password);
        }

        public async Task<PagedList<UserDto>> GetAllUsersAsync(UserParameters userParameters)
        {
            var usersList = await userManager.Users.Select(u => new UserDto { Email = u.Email, UserName = u.UserName }).ToListAsync();
            return PagedList<UserDto>.ToPagedList(usersList, userParameters.PageNumber, userParameters.PageSize);
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            await userManager.DeleteAsync(user);
        }
    }
}
