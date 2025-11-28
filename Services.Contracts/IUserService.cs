using Shared.DataTransferObject.User;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<UserDto> GetUserProfileAsync(string userId);
        Task AddUserAsync(UserRegisterDto model, string role);
        Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUsersAsync(UserParameters userParameters);
        Task DeleteUserAsync(string email);
    }
}
