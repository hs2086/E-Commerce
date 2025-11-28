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
        Task SaveAsync();
    }
}
