using AutoMapper;
using Contracts.IRepository;
using Contracts.Logger;
using Entities.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using Shared.DataTransferObject.Auth;
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

        public async Task RegisterAsync(AuthRegisterDto authRegister)
        {
            if (await repositoryManager.Auth.GetUserByEmailAsync(authRegister.Email) != null)
            {
                throw new EmailAlreadyExistBadRequestException($"Email: {authRegister.Email} is already registered.");
            }
            try
            {
                await repositoryManager.Auth.RegisterUserAsync(authRegister);

                await repositoryManager.Auth.AssignRoleAsync(authRegister.Email, "user");

                await repositoryManager.Auth.SendVerificationEmailAsync(authRegister.Email);
            }
            catch (Exception)
            {
                await repositoryManager.Auth.DeleteUserByEmailAsync(authRegister.Email);
                throw;
            }
        }
        public async Task VerifyEmailAsync(VerifyEmailDto verifyEmail)
        {
            if (await repositoryManager.Auth.GetUserByEmailAsync(verifyEmail.Email) == null)
            {
                throw new EmailNotFoundException($"Email: {verifyEmail.Email} not found.");
            }

            await repositoryManager.Auth.VerifyEmailAsync(verifyEmail);
        }

        public async Task ResendVerificationEmailAsync(ResendVerificationEmailDto resendVerificationEmail)
        {
            var user = await repositoryManager.Auth.GetUserByEmailAsync(resendVerificationEmail.Email);
            if (user != null)
            {
                throw new EmailNotFoundException($"Email: {resendVerificationEmail.Email} not found.");
            }
            if (user.EmailConfirmed)
            {
                throw new EmailAlreadyVerifiedBadRequestException($"Email: {resendVerificationEmail.Email} is already verified.");
            }
            await repositoryManager.Auth.SendVerificationEmailAsync(resendVerificationEmail.Email);
        }

    }
}
