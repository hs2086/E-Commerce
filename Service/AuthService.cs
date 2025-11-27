using AutoMapper;
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
    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthService(
            IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper,
            IConfiguration configuration)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
            this.configuration = configuration;
        }

    }
}
