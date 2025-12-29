using VoroFit.Domain.Entities;
using VoroFit.Domain.Entities.Identity;
using VoroFit.Domain.Entities.Evolution;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VoroFit.Domain.Interfaces.Entities;
using System.Linq.Expressions;

namespace VoroFit.Infrastructure.Factories
{
    public class JasmimDbContext(DbContextOptions<JasmimDbContext> options) : IdentityDbContext<User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(options)
    {

        // Expor explicitamente a entidade de junção
        //public DbSet<Exemplo> Exemplo { get; set; }
        public DbSet<InstanceExtension> InstanceExtensions { get; set; }
        public DbSet<UserExtension> UserExtensions { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<MealPlanMeal> MealPlanMeals { get; set; }
        public DbSet<MealPlanDay> MealPlanDays { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }

        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<WorkoutPlanWeek> WorkoutPlanWeeks { get; set; }
        public DbSet<WorkoutPlanDay> WorkoutPlanDays { get; set; }
        public DbSet<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }

        public DbSet<WorkoutHistory> WorkoutHistories { get; set; }
        public DbSet<WorkoutHistoryExercise> WorkoutHistoryExercises { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Measurement> Measurements { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageReaction> MessageReactions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // ---------------------------
            // USER EXTENSION
            // ---------------------------
            builder.Entity<UserExtension>()
                .HasKey(ue => ue.UserId);

            builder.Entity<UserExtension>()
                .HasOne(ue => ue.User)
                .WithOne(u => u.UserExtension)
                .HasForeignKey<UserExtension>(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InstanceExtension>()
                .HasKey(ue => ue.InstanceId);

            builder.Entity<InstanceExtension>()
                .HasOne(ue => ue.Instance)
                .WithOne(u => u.InstanceExtension)
                .HasForeignKey<InstanceExtension>(ue => ue.InstanceId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------------------
            // STUDENT
            // ---------------------------
            builder.Entity<Student>()
                .HasKey(s => s.UserExtensionId);

            builder.Entity<Student>()
                .HasOne(s => s.UserExtension)
                .WithOne(ue => ue.Student)
                .HasForeignKey<Student>(s => s.UserExtensionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Trainer (UserExtension → Students)
            builder.Entity<Student>()
                .HasOne(s => s.Trainer)
                .WithMany() // um treinador pode ter vários alunos
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------
            // MEASUREMENT
            // ---------------------------
            builder.Entity<Measurement>()
                .HasKey(m => m.Id);

            builder.Entity<Measurement>()
                .HasOne(m => m.Student)
                .WithMany(s => s.Measurements)
                .HasForeignKey(m => m.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------------------
            // MEAL PLAN
            // ---------------------------
            builder.Entity<MealPlan>()
                .HasKey(mp => mp.Id);

            builder.Entity<MealPlan>()
                .HasOne(mp => mp.Student)
                .WithMany(s => s.MealPlans)
                .HasForeignKey(mp => mp.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------------------
            // MEAL PLAN DAY
            // ---------------------------
            builder.Entity<MealPlanDay>()
                .HasKey(mp => mp.Id);

            builder.Entity<MealPlanDay>()
                .Property(mp => mp.DayOfWeek)
                .HasMaxLength(20);

            builder.Entity<MealPlanDay>()
                .HasOne(mp => mp.MealPlan)
                .WithMany(p => p.Days)
                .HasForeignKey(mp => mp.MealPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------------------
            // MEAL PLAN MEAL
            // ---------------------------
            builder.Entity<MealPlanMeal>()
                .HasKey(mpm => mpm.Id);

            builder.Entity<MealPlanMeal>()
                .HasOne(mpm => mpm.MealPlanDay)
                .WithMany(d => d.Meals)
                .HasForeignKey(mpm => mpm.MealPlanDayId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------------------
            // EXERCISE
            // ---------------------------
            builder.Entity<Exercise>()
                .HasKey(e => e.Id);

            // ---------------------------
            // WORKOUT PLAN
            // ---------------------------
            builder.Entity<WorkoutPlan>()
                .HasKey(wp => wp.Id);

            builder.Entity<WorkoutPlan>()
                .HasOne(wp => wp.Student)
                .WithMany(s => s.WorkoutPlans)
                .HasForeignKey(wp => wp.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------------------
            // WORKOUT PLAN WEEK
            // ---------------------------
            builder.Entity<WorkoutPlanWeek>()
                .HasKey(wpw => wpw.Id);

            builder.Entity<WorkoutPlanWeek>()
                .HasOne(wpw => wpw.WorkoutPlan)
                .WithMany(wp => wp.Weeks)
                .HasForeignKey(wpw => wpw.WorkoutPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkoutPlanWeek>()
                .Property(wpw => wpw.WeekNumber)
                .IsRequired();

            // ---------------------------
            // WORKOUT PLAN DAY
            // ---------------------------
            builder.Entity<WorkoutPlanDay>()
                .HasKey(wpd => wpd.Id);

            builder.Entity<WorkoutPlanDay>()
                .HasOne(wpd => wpd.WorkoutPlanWeek)
                .WithMany(wpw => wpw.Days)
                .HasForeignKey(wpd => wpd.WorkoutPlanWeekId)
                .OnDelete(DeleteBehavior.Cascade);

            // ---------------------------
            // WORKOUT PLAN EXERCISE
            // ---------------------------
            builder.Entity<WorkoutPlanExercise>()
                .HasKey(wpe => wpe.Id);

            builder.Entity<WorkoutPlanExercise>()
                .HasOne(wpe => wpe.WorkoutPlanDay)
                .WithMany(wpd => wpd.Exercises)
                .HasForeignKey(wpe => wpe.WorkoutPlanDayId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkoutPlanExercise>()
                .HasOne(wpe => wpe.Exercise)
                .WithMany(e => e.WorkoutPlanExercises)
                .HasForeignKey(wpe => wpe.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------
            // WORKOUT HISTORY
            // ---------------------------
            builder.Entity<WorkoutHistory>()
                .HasKey(wh => wh.Id);

            builder.Entity<WorkoutHistory>()
                .HasOne(wh => wh.Student)
                .WithMany(s => s.WorkoutHistories)
                .HasForeignKey(wh => wh.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkoutHistory>()
                .HasOne(wh => wh.WorkoutPlan)
                .WithMany()
                .HasForeignKey(wh => wh.WorkoutPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WorkoutHistory>()
                .HasOne(wh => wh.WorkoutPlanWeek)
                .WithMany()
                .HasForeignKey(wh => wh.WorkoutPlanWeekId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WorkoutHistory>()
                .HasOne(wh => wh.WorkoutPlanDay)
                .WithMany()
                .HasForeignKey(wh => wh.WorkoutPlanDayId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------------------
            // WORKOUT HISTORY EXERCISE
            // ---------------------------
            builder.Entity<WorkoutHistoryExercise>()
                .HasKey(whe => whe.Id);

            builder.Entity<WorkoutHistoryExercise>()
                .HasOne(whe => whe.WorkoutHistory)
                .WithMany(wh => wh.Exercises)
                .HasForeignKey(whe => whe.WorkoutHistoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkoutHistoryExercise>()
                .HasOne(whe => whe.Exercise)
                .WithMany(e => e.WorkoutHistoryExercises)
                .HasForeignKey(whe => whe.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // ---------------------------
            // SOFT DELETE GLOBAL FILTER
            // ---------------------------
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(DbContext)
                        .GetMethod(nameof(Set), Type.EmptyTypes)!
                        .MakeGenericMethod(entityType.ClrType);

                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);

                    builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            // ---------------------------
            // IDENTITY CONFIG
            // ---------------------------
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<UserRole>().ToTable("UserRoles");

            builder.Entity<User>(b =>
            {
                b.Property(u => u.FirstName).HasMaxLength(100);
                b.Property(u => u.LastName).HasMaxLength(100);
                b.Property(u => u.CountryCode).HasMaxLength(3);
                b.Property(u => u.CreatedAt).HasDefaultValueSql("TIMEZONE('utc', NOW())");
                b.Property(u => u.IsActive).HasDefaultValue(true);
            });

            builder.Entity<Role>(b =>
            {
                b.Property(r => r.Name).HasMaxLength(256);
            });

            builder.Entity<UserRole>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });

                b.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                b.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        }
    }
}
