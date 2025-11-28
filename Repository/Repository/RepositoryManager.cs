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
        private Lazy<IAuthRepository> _authRepository;
        private Lazy<ICategoryRepository> _categoryRepository;
        private Lazy<IProductRepository> _productRepository;

        public RepositoryManager(RepositoryContext repositoryContext, UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _authRepository = new Lazy<IAuthRepository>(() => new AuthRepository(userManager, configuration, emailService));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryContext));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(repositoryContext));
        }

        public IAuthRepository Auth => _authRepository.Value;

        public IProductRepository Product => _productRepository.Value;

        public ICategoryRepository Category => _categoryRepository.Value;
    }
}
