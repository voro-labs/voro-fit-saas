namespace VoroFit.Application.Services.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }

        bool IsTrainer { get; }
        bool IsAuthenticated { get; }
    }
}
