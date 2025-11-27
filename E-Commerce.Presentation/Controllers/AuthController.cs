using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObject.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager service;

        public AuthController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterDto authRegister)
        {
            await service.AuthService.RegisterAsync(authRegister);

            return Ok("Account created. Please check your email to verify your account.");
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto verifyEmail)
        {
            await service.AuthService.VerifyEmailAsync(verifyEmail);
            return Ok("Email verified successfully.");
        }

        [HttpPost("resend-verification-email")]
        public async Task<IActionResult> ResendVerificationEmail([FromBody] ResendVerificationEmailDto resendVerificationEmail)
        {
            await service.AuthService.ResendVerificationEmailAsync(resendVerificationEmail);
            return Ok("Verification email has been resent.");
        }


    }
}
