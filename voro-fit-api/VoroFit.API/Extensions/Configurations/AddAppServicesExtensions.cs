using VoroFit.Application.Services;
using VoroFit.Application.Services.Evolution;
using VoroFit.Application.Services.Identity;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Application.Services.Interfaces.Email;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Application.Services.Interfaces.Identity;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Domain.Interfaces.Repositories.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Identity;
using VoroFit.Infrastructure.Email;
using VoroFit.Infrastructure.Repositories;
using VoroFit.Infrastructure.Repositories.Identity;
using VoroFit.Infrastructure.Seeds;
using VoroFit.Infrastructure.UnitOfWork;
using VoroFit.Shared.Utils;

namespace VoroFit.API.Extensions.Configurations
{
    public static class AddAppServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailUtil>(configuration.GetSection("EmailSettings"));
            services.Configure<CookieUtil>(configuration.GetSection("CookieSettings"));
            services.Configure<EvolutionUtil>(configuration.GetSection("EvolutionSettings"));

            #region Identity Repositories
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IContactIdentifierRepository, ContactIdentifierRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageReactionRepository, MessageReactionRepository>();
            services.AddScoped<IGroupMemberRepository, GroupMemberRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IInstanceRepository, InstanceRepository>();

            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IMealPlanRepository, MealPlanRepository>();
            services.AddScoped<IMealPlanDayRepository, MealPlanDayRepository>();
            services.AddScoped<IMealPlanMealRepository, MealPlanMealRepository>();
            services.AddScoped<IMeasurementRepository, MeasurementRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IWorkoutHistoryRepository, WorkoutHistoryRepository>();
            #endregion

            #region Identity Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IContactIdentifierService, ContactIdentifierService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessageReactionService, MessageReactionService>();
            services.AddScoped<IGroupMemberService, GroupMemberService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IInstanceService, InstanceService>();

            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IMealPlanService, MealPlanService>();
            services.AddScoped<IMealPlanDayService, MealPlanDayService>();
            services.AddScoped<IMealPlanMealService, MealPlanMealService>();
            services.AddScoped<IMeasurementService, MeasurementService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IWorkoutHistoryService, WorkoutHistoryService>();

            #endregion

            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEvolutionService, EvolutionService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IMailKitEmailService, MailKitEmailService>();

            services.AddSignalR();

            return services;
        }
    }
}
