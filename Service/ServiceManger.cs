using AutoMapper;
using Contracts;
using Contracts.IRepository;
using Contracts.Logger;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
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
        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerManager logger, IMapper mapper,
            IConfiguration configuration)
        {
            _authService = new Lazy<IAuthService>(() => new AuthService(repositoryManager, logger, mapper, configuration));
        }

        public IAuthService AuthService => _authService.Value;
    }
}
