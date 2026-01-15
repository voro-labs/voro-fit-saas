using VoroFit.Application.DTOs;
using VoroFit.Domain.Entities.Identity;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IAuthService
    {
        // Autenticação
        Task<AuthDto> SignInAsync(SignInDto signInDto);

        // Registro de usuário
        Task<User> SignUpAsync(SignUpDto signUpDto, ICollection<string> roles);

        // Confirmação de e-mail
        Task ConfirmEmailAsync(string email);
        Task<bool> ConfirmEmailAsync(AuthDto authDto, string email);

        // Recuperação de senha
        Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}
