using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObject.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IAuthRepository
    {
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task RegisterUserAsync(AuthRegisterDto authRegister);
        Task AssignRoleAsync(string email, string role);
        Task SendVerificationEmailAsync(string email);
        Task DeleteUserByEmailAsync(string email);
        Task VerifyEmailAsync(VerifyEmailDto verifyEmail);
        Task<AuthModel> LoginAsync(LoginDto login);
        Task<AuthModel> RefreshTokenAsync(RefreshTokenDto refreshToken);
        Task LogoutAsync(string? userId);
    }
}
