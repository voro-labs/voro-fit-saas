using VoroFit.API.Extensions;
using VoroFit.API.ViewModels;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VoroFit.API.Controllers
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Identity")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
        {
            try
            {
                var authDto = await _authService.SignInAsync(signInDto);

                return ResponseViewModel<AuthDto>
                    .SuccessWithMessage("Sign-in successful.", authDto)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<AuthDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            try
            {
                var errors = await _authService.SignUpAsync(signUpDto, [RoleConstant.User]);

                if (errors.Any())
                {
                    var ex = new Exception(string.Join("; ", errors.Select(e => e.Description)));

                    return ResponseViewModel<IEnumerable<IdentityError>>
                        .Fail(ex.Message, errors)
                        .ToActionResult();
                }

                return ResponseViewModel<IEnumerable<IdentityError>>
                    .SuccessWithMessage("Sign-up successful.", [])
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<IEnumerable<IdentityError>>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                var errors = await _authService.ForgotPasswordAsync(forgotPasswordDto);

                if (errors.Any())
                {
                    var ex = new Exception(string.Join("; ", errors.Select(e => e.Description)));

                    return ResponseViewModel<IEnumerable<IdentityError>>
                        .Fail(ex.Message, errors)
                        .ToActionResult();
                }

                return ResponseViewModel<IEnumerable<IdentityError>>
                    .SuccessWithMessage("Forgot-password successful.", [])
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<IEnumerable<IdentityError>>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var errors = await _authService.ResetPasswordAsync(resetPasswordDto);

                if (errors.Any())
                {
                    var ex = new Exception(string.Join("; ", errors.Select(e => e.Description)));

                    return ResponseViewModel<IEnumerable<IdentityError>>
                        .Fail(ex.Message, errors)
                        .ToActionResult();
                }

                return ResponseViewModel<IEnumerable<IdentityError>>
                    .SuccessWithMessage("Reset-password successful.", [])
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<IEnumerable<IdentityError>>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }
    }
}
