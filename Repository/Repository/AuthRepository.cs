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
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;

        public AuthRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.emailService = emailService;
        }
    }
}
