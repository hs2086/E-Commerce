using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IAuthService AuthService { get; }
        ICategoryService CategoryService { get; }
        IProductService ProductService { get; }
        IRoleService RoleService { get; }
        IUserService UserService { get; }
        ICartService CartService { get; }
    }
}
