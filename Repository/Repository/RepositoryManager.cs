using Contracts.IRepository;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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

        public RepositoryManager(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _authRepository = new Lazy<IAuthRepository>(() => new AuthRepository(userManager, configuration, emailService));
        }

        public IAuthRepository Auth => _authRepository.Value;
    }
}
