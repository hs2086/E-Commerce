using Entities.Model;
using Shared.DataTransferObject.User;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserByIdAsync(string userId);
        Task AddUserAsync(ApplicationUser user, string password);
        Task<PagedList<UserDto>> GetAllUsersAsync(UserParameters userParameters);
        Task DeleteUserAsync(ApplicationUser user);
    }
}
