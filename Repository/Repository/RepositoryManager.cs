using Contracts.IRepository;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.Data;
using Repository.Repos;
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
        private Lazy<ICartRepository> _cartRepository;
        private Lazy<ICheckoutRepository> _checkoutRepository;
        private Lazy<IOrderRepository> _orderRepository;
        private Lazy<IPaymentRepository> _paymentRepository;
        private Lazy<IReviewRepository> _reviewRepository;
        private Lazy<IWishlistRepository> _wishlistRepository;

        public RepositoryManager(RepositoryContext repositoryContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            this.repositoryContext = repositoryContext;
            _authRepository = new Lazy<IAuthRepository>(() => new AuthRepository(userManager, configuration, emailService));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryContext));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(repositoryContext));
            _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(roleManager));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(userManager));
            _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(repositoryContext));
            _checkoutRepository = new Lazy<ICheckoutRepository>(() => new CheckoutRepository(repositoryContext));
            _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(repositoryContext));
            _paymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(repositoryContext));
            _reviewRepository = new Lazy<IReviewRepository>(() => new ReviewRepository(repositoryContext));
            _wishlistRepository = new Lazy<IWishlistRepository>(() => new WishlistRepository(repositoryContext));
        }

        public IAuthRepository Auth => _authRepository.Value;

        public IProductRepository Product => _productRepository.Value;

        public ICategoryRepository Category => _categoryRepository.Value;

        public IRoleRepository Role => _roleRepository.Value;

        public IUserRepository User => _userRepository.Value;

        public ICartRepository Cart => _cartRepository.Value;

        public ICheckoutRepository Checkout => _checkoutRepository.Value;

        public IOrderRepository Order => _orderRepository.Value;

        public IPaymentRepository Payment => _paymentRepository.Value;

        public IReviewRepository Review => _reviewRepository.Value;

        public IWishlistRepository Wishlist => _wishlistRepository.Value;

        public async Task SaveAsync() => await repositoryContext.SaveChangesAsync();
    }
}
