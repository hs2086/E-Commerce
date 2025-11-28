using AutoMapper;
using Contracts;
using Contracts.IRepository;
using Contracts.Logger;
using Microsoft.Extensions.Configuration;
using Service.DataShapping;
using Services.Contracts;
using Shared.DataTransferObject.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IRoleService> _roleService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<ICartService> _cartService;
        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerManager logger, IMapper mapper,
            IDataShaper<ProductDto> dataShaper,
            IConfiguration configuration)
        {
            _authService = new Lazy<IAuthService>(() => new AuthService(repositoryManager, configuration));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, logger, mapper));
            _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, logger, mapper, configuration, dataShaper));
            _roleService = new Lazy<IRoleService>(() => new RoleService(repositoryManager, logger, mapper));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, logger, mapper));
            _cartService = new Lazy<ICartService>(() => new CartService(repositoryManager, logger, mapper));
        }

        public IAuthService AuthService => _authService.Value;

        public ICategoryService CategoryService => _categoryService.Value;

        public IProductService ProductService => _productService.Value;

        public IRoleService RoleService => _roleService.Value;

        public IUserService UserService => _userService.Value;

        public ICartService CartService => _cartService.Value;
    }
}
