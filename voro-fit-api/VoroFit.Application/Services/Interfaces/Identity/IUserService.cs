using VoroFit.Application.DTOs;
using VoroFit.Domain.Entities.Identity;
using VoroFit.Application.DTOs.Identity;
using VoroFit.Application.Services.Interfaces.Base;

namespace VoroFit.Application.Services.Interfaces.Identity
{
    public interface IUserService : IServiceBase<User>
    {
        Task<(User user, IList<string>? rolesNames)> GetByEmailAndPassword(string email, string password);
        Task<User> CreateAsync(StudentDto dto, string password, ICollection<string> roles);
        Task<User> CreateAsync(UserDto dto, string password, ICollection<string> roles);
        Task<User> UpdateAsync(Guid id, UserDto dto);
        Task<(User user, string token)> GenerateConfirmEmailAsync(string email);
        Task<bool> ConfirmEmailAsync(AuthDto authViewModel, string email);
        Task<(User user, string token)> GenerateForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}
