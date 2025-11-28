using Contracts.IRepository;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.Data;
using Repository.Repos.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext repositoryContext;
        private Lazy<IAuthRepository> _authRepository;
        private Lazy<ICategoryRepository> _categoryRepository;
        private Lazy<IProductRepository> _productRepository;
        private Lazy<IRoleRepository> _roleRepository;
        private Lazy<IUserRepository> _userRepository;

        public RepositoryManager(RepositoryContext repositoryContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            _authRepository = new Lazy<IAuthRepository>(() => new AuthRepository(userManager, configuration, emailService));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryContext));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(repositoryContext));
            this.repositoryContext = repositoryContext;
            _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(roleManager));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(userManager));
        }

        public IAuthRepository Auth => _authRepository.Value;

        public IProductRepository Product => _productRepository.Value;

        public ICategoryRepository Category => _categoryRepository.Value;

        public IRoleRepository Role => _roleRepository.Value;

        public IUserRepository User => _userRepository.Value;

        public async Task SaveAsync() => await repositoryContext.SaveChangesAsync();
    }
}
