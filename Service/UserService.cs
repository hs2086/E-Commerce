using AutoMapper;
using Contracts.IRepository;
using Contracts.Logger;
using Entities.Exceptions;
using Entities.Model;
using Services.Contracts;
using Shared.DataTransferObject.User;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public UserService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<UserDto> GetUserProfileAsync(string userId)
        {
            var user = await repositoryManager.User.GetUserByIdAsync(userId);
            return user;
        }

        public async Task AddUserAsync(UserRegisterDto model, string role)
        {
            ApplicationUser user = await repositoryManager.Auth.GetUserByEmailAsync(model.Email);
            if (user != null)
                throw new EmailAlreadyExistBadRequestException(model.Email);
            var newUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                EmailConfirmed = true
            };
            await repositoryManager.User.AddUserAsync(newUser, model.Password);
            await repositoryManager.Auth.AssignRoleAsync(newUser.Email, role);
        }

        public async Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUsersAsync(UserParameters userParameters)
        {
            var usersWithMetaData = await repositoryManager.User.GetAllUsersAsync(userParameters);
            return (users: usersWithMetaData, metaData: usersWithMetaData.MetaData);
        }

        public async Task DeleteUserAsync(string email)
        {
            var user = await repositoryManager.Auth.GetUserByEmailAsync(email);
            if (user == null)
                throw new UserNotFoundException(email);
            await repositoryManager.User.DeleteUserAsync(user);
        }
    }
}
