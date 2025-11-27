using Contracts.IRepository;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Repository.Repos.Email;
using Shared.DataTransferObject.Auth;
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

        public async Task RegisterUserAsync(AuthRegisterDto authRegister)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = authRegister.Email,
                UserName = authRegister.Email
            };

            IdentityResult result = await userManager.CreateAsync(user, authRegister.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task AssignRoleAsync(string email, string role)
        {
            await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(email) ?? new ApplicationUser(), role);
        }
        public async Task SendVerificationEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email) ?? new ApplicationUser();
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            string url = $"{configuration["JWT:Frontend"]}/verify-email?email={user.Email}&token={encodedToken}";

            await emailService.SendEmailAsync(
                user.Email ?? "",
                "Verify Your Email",
                $@"
                <p>Hello,</p>
                <p>Thank you for registering. Please verify your email by <a href=""{url}"">clicking here</a>.</p>
                <p>If you did not register, please ignore this email.</p>
                <br/>
                <p>Best regards,<br/>The Support Team</p>"
            );
        }
        public async Task DeleteUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email) ?? new ApplicationUser();
            await userManager.DeleteAsync(user);
        }

        public async Task VerifyEmailAsync(VerifyEmailDto verifyEmail)
        {
            var user = await userManager.FindByEmailAsync(verifyEmail.Email) ?? new ApplicationUser();
            var decodedBytes = WebEncoders.Base64UrlDecode(verifyEmail.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedBytes);
            IdentityResult result = await userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
            {
                throw new Exception("Email verification failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

    }
}
