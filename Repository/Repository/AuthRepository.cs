using Contracts.IRepository;
using Entities.Exceptions;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Repos.Email;
using Shared.DataTransferObject.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            List<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            userClaims.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));
            userClaims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            IList<String> roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? ""));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JWT:DurationInMinutes"])),
                signingCredentials: credentials
                );
            return jwtSecurityToken;
        }
        private string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "") + Guid.NewGuid().ToString().Replace("-", "");
        }
        public async Task<AuthModel> LoginAsync(LoginDto login)
        {
            var user = await userManager.FindByEmailAsync(login.Email) ?? new ApplicationUser();
            if (user == null || !await userManager.CheckPasswordAsync(user, login.Password))
            {
                throw new UserNotFoundException($"{login.Email} not found.");
            }
            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                await SendVerificationEmailAsync(login.Email);
                throw new EmailNotVerifiedBadRequestException("Email is not verified, And we sent the verification to your email.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            user.RefreshToken = CreateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(configuration["JWT:DurationInDays"] ?? ""));

            await userManager.UpdateAsync(user);

            return new AuthModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expiration = jwtSecurityToken.ValidTo.ToLocalTime(),
                RefreshToken = user.RefreshToken
            };
        }
        public async Task<AuthModel> RefreshTokenAsync(RefreshTokenDto refreshToken)
        {
            var user = await userManager.FindByEmailAsync(refreshToken.Email) ?? new ApplicationUser();
            if (user.RefreshToken != refreshToken.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new InvalidRefreshTokenBadRequestException("Invalid refresh token.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            user.RefreshToken = CreateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(configuration["JWT:DurationInDays"] ?? ""));
            await userManager.UpdateAsync(user);
            return new AuthModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expiration = jwtSecurityToken.ValidTo.ToLocalTime(),
                RefreshToken = user.RefreshToken
            };
        }
        public async Task LogoutAsync(string? userId)
        {
            if (userId == null) return;
            var user = await userManager.FindByIdAsync(userId) ?? new ApplicationUser();
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.Now;
            await userManager.UpdateAsync(user);
        }
        public async Task SendPasswordResetEmailAsync(ApplicationUser user)
        {
            string token = await userManager.GeneratePasswordResetTokenAsync(user);
            string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            string url = $"{configuration["JWT:Frontend"]}/reset-password?email={user.Email}&token={encodedToken}";
            await emailService.SendEmailAsync(
                user.Email ?? "",
                "Reset Your Password",
                $@"
                <p>Hello,</p>
                <p>You requested to reset your password. Please reset your password by <a href=""{url}"">clicking here</a>.</p>
                <p>If you did not request a password reset, please ignore this email.</p>
                <br/>
                <p>Best regards,<br/>The Support Team</p>"
            );
        }
        public async Task ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            var user = await userManager.FindByEmailAsync(resetPassword.Email) ?? new ApplicationUser();
            var decodedBytes = WebEncoders.Base64UrlDecode(resetPassword.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedBytes);
            await userManager.ResetPasswordAsync(user, decodedToken, resetPassword.NewPassword);
        }
        public async Task ChangePasswordAsync(ChangePasswordDto changePassword, string userId)
        {
            var user = await userManager.FindByIdAsync(userId) ?? new ApplicationUser();
            await userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
        }
        public async Task<IEnumerable<IdentityRole>> GetRolesAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email) ?? new ApplicationUser();
            var roles = await userManager.GetRolesAsync(user);
            List<IdentityRole> identityRoles = new List<IdentityRole>();
            foreach (var roleName in roles)
            {
                identityRoles.Add(new IdentityRole { Name = roleName });
            }
            return identityRoles;
        }
        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }
    }
}
