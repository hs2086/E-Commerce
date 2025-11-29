using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IRepositoryManager
    {
        IAuthRepository Auth { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IRoleRepository Role { get; }
        IUserRepository User { get; }
        ICartRepository Cart { get; }
        ICheckoutRepository Checkout { get; }
        IOrderRepository Order { get; }
        IPaymentRepository Payment { get; }
        IReviewRepository Review { get; }
        IWishlistRepository Wishlist { get; }
        Task SaveAsync();
    }
}
