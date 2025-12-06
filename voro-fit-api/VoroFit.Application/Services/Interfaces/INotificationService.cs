using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Interfaces.Base;

namespace VoroFit.Application.Services.Interfaces
{
    public interface INotificationService : IServiceBase<Notification>
    {
        Task SendWelcomeAsync(string email, string userName);

        Task SendResetLinkAsync(string email, string userName, string token);
    }
}
