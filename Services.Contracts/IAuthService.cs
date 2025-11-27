using Shared.DataTransferObject.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAuthService
    {
        Task RegisterAsync(AuthRegisterDto authRegister);
        Task VerifyEmailAsync(VerifyEmailDto verifyEmail);
        Task ResendVerificationEmailAsync(ResendVerificationEmailDto resendVerificationEmail);
        Task<AuthModel> LoginAsync(LoginDto login);
        Task<AuthModel> RefreshTokenAsync(RefreshTokenDto refreshToken);
        Task LogoutAsync(string? userId);
    }
}
