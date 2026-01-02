using Microsoft.EntityFrameworkCore;
using System.Data;
using VoroFit.Domain.Entities;
using VoroFit.Domain.Entities.Identity;
using VoroFit.Domain.Enums;
using VoroFit.Infrastructure.Factories;
using VoroFit.Shared.Constants;
using VoroFit.Shared.Extensions;

namespace VoroFit.Infrastructure.Seeds
{
    public class DataSeeder : IDataSeeder
    {
        public async Task SeedAsync(JasmimDbContext context)
        {
            // Garante que o banco existe e está migrado
            await context.Database.MigrateAsync();

            // SEED: Notifications
            SeedNotifications(context);

            await context.SaveChangesAsync();

            // SEED: Roles
            SeedRoles(context);

            await context.SaveChangesAsync();

            // SEED: Usuário Admin
            SeedUsers(context);

            await context.SaveChangesAsync();

            // SEED: Students
            SeedStudents(context);

            await context.SaveChangesAsync();

            // SEED: Exercises
            SeedExercises(context);

            await context.SaveChangesAsync();

            // // SEED: Measurements
            // SeedMeasurements(context);

            // await context.SaveChangesAsync();

            // // SEED: Workout Plans
            // SeedWorkoutPlans(context);

            // await context.SaveChangesAsync();

            // // SEED: Workout Plan Weeks
            // SeedWorkoutPlanWeeks(context);

            // await context.SaveChangesAsync();

            // // SEED: Workout Plan Days
            // SeedWorkoutPlanDays(context);

            // await context.SaveChangesAsync();

            // // SEED: Workout Plan Exercises
            // SeedWorkoutPlanExercises(context);

            // await context.SaveChangesAsync();

            // // SEED: Workout Histories
            // SeedWorkoutHistories(context);

            // await context.SaveChangesAsync();

            // // SEED: Workout History Exercises
            // SeedWorkoutHistoryExercises(context);

            // await context.SaveChangesAsync();

            // // SEED: Meal Plans
            // SeedMealPlans(context);

            // await context.SaveChangesAsync();

            // // SEED: Meal Plan Days
            // SeedMealPlanDays(context);

            // await context.SaveChangesAsync();

            // // SEED: Meal Plan Meals
            // SeedMealPlanMeals(context);

            // await context.SaveChangesAsync();
        }

        private static void SeedNotifications(JasmimDbContext context)
        {
            if (!context.Notifications.Any())
            {
                var notifications = new List<Notification>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = NotificationEnum.Welcome.AsText(),
                        Subject = "Bem-vindo(a) ao sistema, {UserName}!",
                        Body = @"
                            <div style='font-family: Arial, sans-serif; background-color: #f7f7f7; padding: 30px;'>
                                <div style='max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 8px; padding: 30px; box-shadow: 0 2px 5px rgba(0,0,0,0.05);'>
                                    <h2 style='color: #333333; text-align: center;'>Bem-vindo(a), {UserName}!</h2>
                                    <p style='color: #555555; font-size: 16px;'>Olá <strong>{UserName}</strong>,</p>
                                    <p style='color: #555555; font-size: 16px;'>
                                        Sua conta foi criada com sucesso! Estamos muito felizes em tê-lo(a) conosco.
                                    </p>
                                    <p style='color: #555555; font-size: 16px;'>
                                        Explore os recursos disponíveis e aproveite ao máximo sua experiência no sistema.
                                    </p>
                                    <br/>
                                    <p style='font-size: 15px; color: #888888; text-align: center;'>
                                        Atenciosamente,<br/>
                                        <strong>Equipe Suporte</strong>
                                    </p>
                                </div>
                            </div>",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = NotificationEnum.PasswordReset.AsText(),
                        Subject = "Redefinição de Senha - {UserName}",
                        Body = @"
                            <div style='font-family: Arial, sans-serif; background-color: #f7f7f7; padding: 30px;'>
                                <div style='max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 8px; padding: 30px; box-shadow: 0 2px 5px rgba(0,0,0,0.05);'>
                                    <h2 style='color: #333333; text-align: center;'>Redefinição de Senha</h2>
                                    <p style='color: #555555; font-size: 16px;'>Olá <strong>{UserName}</strong>,</p>
                                    <p style='color: #555555; font-size: 16px;'>
                                        Recebemos uma solicitação para redefinir sua senha. Para continuar, clique no botão abaixo:
                                    </p>
                                    <div style='text-align: center; margin: 25px 0;'>
                                        <a href='{ResetLink}' style='background-color: #4CAF50; color: white; padding: 12px 24px; text-decoration: none; border-radius: 6px; font-weight: bold;'>
                                            Redefinir Senha
                                        </a>
                                    </div>
                                    <p style='color: #777777; font-size: 14px;'>
                                        Se você não solicitou essa alteração, basta ignorar este e-mail.
                                    </p>
                                    <br/>
                                    <p style='font-size: 15px; color: #888888; text-align: center;'>
                                        Atenciosamente,<br/>
                                        <strong>Equipe Suporte</strong>
                                    </p>
                                </div>
                            </div>",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = NotificationEnum.ConfirmEmail.AsText(),
                        Subject = "Confirmação de E-mail - {UserName}",
                        Body = @"
                            <div style='font-family: Arial, sans-serif; background-color: #f7f7f7; padding: 30px;'>
                                <div style='max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 8px; padding: 30px; box-shadow: 0 2px 5px rgba(0,0,0,0.05);'>
                                    <h2 style='color: #333333; text-align: center;'>Confirmação de E-mail</h2>
                                    <p style='color: #555555; font-size: 16px;'>Olá <strong>{UserName}</strong>,</p>
                                    <p style='color: #555555; font-size: 16px;'>
                                        Recebemos uma solicitação para confirmar seu e-mail. Para continuar, clique no botão abaixo:
                                    </p>
                                    <div style='text-align: center; margin: 25px 0;'>
                                        <a href='{ConfirmLink}' style='background-color: #4CAF50; color: white; padding: 12px 24px; text-decoration: none; border-radius: 6px; font-weight: bold;'>
                                            Confirmar E-mail
                                        </a>
                                    </div>
                                    <p style='color: #777777; font-size: 14px;'>
                                        Se você não solicitou essa alteração, basta ignorar este e-mail.
                                    </p>
                                    <br/>
                                    <p style='font-size: 15px; color: #888888; text-align: center;'>
                                        Atenciosamente,<br/>
                                        <strong>Equipe Suporte</strong>
                                    </p>
                                </div>
                            </div>",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    }
                };

                context.Notifications.AddRange(notifications);
            }
        }

        private static void SeedRoles(JasmimDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = typeof(RoleConstant)
                    .GetFields(System.Reflection.BindingFlags.Public |
                                System.Reflection.BindingFlags.Static |
                                System.Reflection.BindingFlags.FlattenHierarchy)
                    .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                    .Select(fi => new Role
                    {
                        Id = Guid.Parse((string)fi.GetRawConstantValue()!),
                        Name = fi.Name.ToTitleCase(),
                        NormalizedName = fi.Name.ToUpper()
                    })
                    .ToList();

                context.Roles.AddRange(roles);
            }
        }

        private static void SeedUsers(JasmimDbContext context)
        {
            if (!context.Users.Any())
            {
                var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");

                var admin = new User
                {
                    UserName = "jordan.silva",
                    NormalizedUserName = "jordan.silva".ToUpper(),
                    Email = "jordan@vorolabs.app",
                    NormalizedEmail = "jordan@vorolabs.app".ToUpper(),
                    FirstName = "Jordan",
                    LastName = "Silva",
                    CountryCode = "+55",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    BirthDate = DateTime.UtcNow,
                    SecurityStamp = "f87c07d8-3b68-4e35-b1e9-97c9021cf4e8",
                    UserRoles = [
                        new UserRole()
                        {
                            Role = adminRole
                        }
                    ],
                    UserExtension = new UserExtension()
                };

                context.Users.Add(admin);

                var trainerRole = context.Roles.FirstOrDefault(r => r.Name == "Trainer");

                var trainer = new User
                {
                    UserName = "joao.treinador",
                    NormalizedUserName = "joao.treinador".ToUpper(),
                    Email = "joao@vorolabs.app",
                    NormalizedEmail = "joao@vorolabs.app".ToUpper(),
                    FirstName = "João",
                    LastName = "Treinador",
                    CountryCode = "+55",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    BirthDate = DateTime.UtcNow,
                    SecurityStamp = "f87c07d8-3b68-4e35-b1e9-97c9021cf4e8",
                    UserRoles = [
                        new UserRole()
                        {
                            Role = trainerRole
                        }
                    ],
                    UserExtension = new UserExtension()
                };

                context.Users.Add(trainer);

                var studentRole = context.Roles.FirstOrDefault(r => r.Name == "Student");

                var student = new User
                {
                    UserName = "felipe.estudante",
                    NormalizedUserName = "felipe.estudante".ToUpper(),
                    Email = "felipe@vorolabs.app",
                    NormalizedEmail = "felipe@vorolabs.app".ToUpper(),
                    FirstName = "Felipe",
                    LastName = "Estudante",
                    CountryCode = "+55",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    BirthDate = DateTime.UtcNow,
                    SecurityStamp = "f87c07d8-3b68-4e35-b1e9-97c9021cf4e8",
                    UserRoles = [
                        new UserRole()
                        {
                            Role = studentRole
                        }
                    ],
                    UserExtension = new UserExtension()
                };

                context.Users.Add(student);
            }
        }

        private static void SeedStudents(JasmimDbContext context)
        {
            if (!context.Students.Any())
            {
                var user = context.Users.FirstOrDefault(u => u.UserRoles.Any(ur => ur.RoleId.ToString() == RoleConstant.Student));
                if (user == null) return;

                var trainer = context.Users.FirstOrDefault(u => u.UserRoles.Any(ur => ur.RoleId.ToString() == RoleConstant.Trainer));

                var students = new List<Student>
                {
                    new()
                    {
                        UserExtensionId = user.Id,
                        TrainerId = trainer?.Id,
                        Age = 25,
                        Height = 175,
                        Weight = 72,
                        Goal = "Hipertrofia",
                        Status = StudentStatusEnum.Active
                    }
                };

                context.Students.AddRange(students);
            }
        }

        private static void SeedExercises(JasmimDbContext context)
        {
            if (!context.Exercises.Any())
            {
                var exercises = new List<Exercise>
                {
                    // ---------------------------
                    // PEITO
                    // ---------------------------
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Supino Reto",
                        MuscleGroup = "Peito",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/supino_reto.jpg",
                        Description = "Execução clássica para trabalhar peitoral maior.",
                        Notes = "Mantenha os ombros retraídos.",
                        Alternatives = "Supino Inclinado, Supino Declinado"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Supino Inclinado",
                        MuscleGroup = "Peito",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/supino_inclinado.jpg",
                        Description = "Foco no peitoral superior.",
                        Alternatives = "Supino Reto, Crucifixo Inclinado"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Crucifixo com Halteres",
                        MuscleGroup = "Peito",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/crucifixo.jpg",
                        Description = "Alongamento e contração profunda do peitoral."
                    },

                    // ---------------------------
                    // COSTAS
                    // ---------------------------
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Barra Fixa",
                        MuscleGroup = "Costas",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/barra_fixa.jpg",
                        Description = "Trabalha dorsal e bíceps.",
                        Notes = "Evite balanço excessivo."
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Puxada Frontal",
                        MuscleGroup = "Costas",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/puxada_frontal.jpg",
                        Description = "Alternativa mais acessível à barra fixa."
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Remada Curvada",
                        MuscleGroup = "Costas",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/remada_curvada.jpg",
                        Description = "Ótimo para espessura do dorso."
                    },

                    // ---------------------------
                    // PERNAS
                    // ---------------------------
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Agachamento Livre",
                        MuscleGroup = "Pernas",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/agachamento.jpg",
                        Description = "Exercício completo para quadríceps e glúteos.",
                        Notes = "Priorize técnica antes de carga."
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Leg Press",
                        MuscleGroup = "Pernas",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/leg_press.jpg",
                        Description = "Trabalha quadríceps com controle de movimento."
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cadeira Extensora",
                        MuscleGroup = "Pernas",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/extensora.jpg",
                        Description = "Isolamento de quadríceps.",
                    },

                    // ---------------------------
                    // BÍCEPS
                    // ---------------------------
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Rosca Direta",
                        MuscleGroup = "Bíceps",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/rosca_direta.jpg",
                        Description = "Movimento básico para bíceps."
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Rosca Alternada",
                        MuscleGroup = "Bíceps",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/rosca_alternada.jpg"
                    },

                    // ---------------------------
                    // TRÍCEPS
                    // ---------------------------
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tríceps Corda",
                        MuscleGroup = "Tríceps",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/triceps_corda.jpg"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tríceps Testa",
                        MuscleGroup = "Tríceps",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/triceps_testa.jpg"
                    },

                    // ---------------------------
                    // OMBROS
                    // ---------------------------
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Desenvolvimento com Halteres",
                        MuscleGroup = "Ombros",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/desenvolvimento.jpg"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Elevação Lateral",
                        MuscleGroup = "Ombros",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/elevacao_lateral.jpg"
                    },

                    // ---------------------------
                    // ABDÔMEN
                    // ---------------------------
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Abdominal Supra",
                        MuscleGroup = "Abdômen",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/abdominal_supra.jpg"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Prancha Isométrica",
                        MuscleGroup = "Abdômen",
                        Type = ExerciseTypeEnum.Public,
                        Thumbnail = "/images/exercises/prancha.jpg"
                    }
                };

                context.Exercises.AddRange(exercises);
            }
        }

        private static void SeedMeasurements(JasmimDbContext context)
        {
            if (!context.Measurements.Any())
            {
                var student = context.Students.FirstOrDefault();
                if (student == null) return;

                var measurements = new List<Measurement>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StudentId  = student.UserExtensionId,
                        Date = DateTime.UtcNow.AddDays(-5),
                        Weight = 72,
                        Waist = 82,
                        Chest = 98,
                        Arms = 33
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StudentId  = student.UserExtensionId,
                        Date = DateTime.UtcNow,
                        Weight = 73,
                        Waist = 81,
                        Chest = 99,
                        Arms = 34
                    }
                };

                context.Measurements.AddRange(measurements);
            }
        }

        private static void SeedWorkoutPlans(JasmimDbContext context)
        {
            if (context.WorkoutPlans.Any()) return;

            var student = context.Students.FirstOrDefault();
            if (student == null) return;

            var plan = new WorkoutPlan
            {
                Id = Guid.NewGuid(),
                StudentId = student.UserExtensionId,
                Status = WorkoutPlanStatusEnum.Active,
                CreatedAt = DateTime.UtcNow
            };

            context.WorkoutPlans.Add(plan);
        }

        private static void SeedWorkoutPlanWeeks(JasmimDbContext context)
        {
            if (context.WorkoutPlanWeeks.Any()) return;

            var plan = context.WorkoutPlans.FirstOrDefault();
            if (plan == null) return;

            var weeks = new List<WorkoutPlanWeek>
    {
        new() { WorkoutPlanId = plan.Id, WeekNumber = 1 },
        new() { WorkoutPlanId = plan.Id, WeekNumber = 2 }
    };

            context.WorkoutPlanWeeks.AddRange(weeks);
        }

        private static void SeedWorkoutPlanDays(JasmimDbContext context)
        {
            if (context.WorkoutPlanDays.Any()) return;

            var weeks = context.WorkoutPlanWeeks.ToList();
            if (!weeks.Any()) return;

            var daysOfWeek = new[] { DayOfWeekEnum.Monday, DayOfWeekEnum.Wednesday, DayOfWeekEnum.Friday };
            var days = new List<WorkoutPlanDay>();

            foreach (var week in weeks)
            {
                foreach (var day in daysOfWeek)
                {
                    days.Add(new WorkoutPlanDay
                    {
                        WorkoutPlanWeekId = week.Id,
                        DayOfWeek = day
                    });
                }
            }

            context.WorkoutPlanDays.AddRange(days);
        }

        private static void SeedWorkoutPlanExercises(JasmimDbContext context)
        {
            if (context.WorkoutPlanExercises.Any()) return;

            var day = context.WorkoutPlanDays.FirstOrDefault();
            if (day == null) return;

            var exercises = context.Exercises.ToList();
            if (!exercises.Any()) return;

            var supino = exercises.First(e => e.Name == "Supino Reto");
            var barra = exercises.First(e => e.Name == "Barra Fixa");

            var planExercises = new List<WorkoutPlanExercise>
    {
        new()
        {
            WorkoutPlanDayId = day.Id,
            ExerciseId = supino.Id,
            Order = 1,
            Sets = 3,
            Reps = 10,
            RestInSeconds = 90,
            Weight = 40
        },
        new()
        {
            WorkoutPlanDayId = day.Id,
            ExerciseId = barra.Id,
            Order = 2,
            Sets = 3,
            Reps = 8,
            RestInSeconds = 120,
            Weight = null
        }
    };

            context.WorkoutPlanExercises.AddRange(planExercises);
        }

        private static void SeedWorkoutHistories(JasmimDbContext context)
        {
            if (context.WorkoutHistories.Any()) return;

            var student = context.Students.FirstOrDefault();
            var plan = context.WorkoutPlans.FirstOrDefault();
            var week = context.WorkoutPlanWeeks.FirstOrDefault();
            var day = context.WorkoutPlanDays.FirstOrDefault();

            if (student == null || plan == null || week == null || day == null) return;

            var history = new WorkoutHistory
            {
                Id = Guid.NewGuid(),
                StudentId = student.UserExtensionId,
                WorkoutPlanId = plan.Id,
                WorkoutPlanWeekId = week.Id,
                WorkoutPlanDayId = day.Id,
                ExecutionDate = DateTime.UtcNow.AddDays(-1),
                Status = WorkoutExecutionStatusEnum.Completed
            };

            context.WorkoutHistories.Add(history);
        }

        private static void SeedWorkoutHistoryExercises(JasmimDbContext context)
        {
            if (context.WorkoutHistoryExercises.Any()) return;

            var history = context.WorkoutHistories.FirstOrDefault();
            if (history == null) return;

            var planned = context.WorkoutPlanExercises.ToList();
            if (!planned.Any()) return;

            var historyExercises = planned.Select(p => new WorkoutHistoryExercise
            {
                WorkoutHistoryId = history.Id,
                ExerciseId = p.ExerciseId,
                Order = p.Order,
                PlannedSets = p.Sets,
                PlannedReps = p.Reps,
                ExecutedSets = p.Sets,
                ExecutedReps = p.Reps - 1, // exemplo realista
                PlannedWeight = p.Weight,
                ExecutedWeight = p.Weight,
                RestInSeconds = p.RestInSeconds
            }).ToList();

            context.WorkoutHistoryExercises.AddRange(historyExercises);
        }

        private static void SeedMealPlans(JasmimDbContext context)
        {
            if (!context.MealPlans.Any())
            {
                var student = context.Students.FirstOrDefault();
                if (student == null) return;

                // ------------------------
                // CRIA O MEAL PLAN
                // ------------------------
                var mealPlan = new MealPlan
                {
                    Status = MealPlanStatusEnum.Active,
                    StudentId = student.UserExtensionId,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };

                context.MealPlans.Add(mealPlan);
            }
        }

        private static void SeedMealPlanDays(JasmimDbContext context)
        {
            if (!context.MealPlanDays.Any())
            {
                var student = context.Students.FirstOrDefault();
                if (student == null) return;

                var mealPlan = context.MealPlans.FirstOrDefault();
                if (mealPlan == null) return;

                // ------------------------
                // DIAS DA SEMANA
                // ------------------------
                var days = new[]
                {
                    DayOfWeekEnum.Monday,
                    DayOfWeekEnum.Tuesday,
                    DayOfWeekEnum.Wednesday,
                    DayOfWeekEnum.Thursday,
                    DayOfWeekEnum.Friday,
                    DayOfWeekEnum.Saturday,
                    DayOfWeekEnum.Sunday
                };

                var mealPlanDays = new List<MealPlanDay>();

                foreach (var day in days)
                {
                    mealPlanDays.Add(new MealPlanDay
                    {
                        DayOfWeek = day,
                        MealPlanId = mealPlan.Id,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                context.MealPlanDays.AddRange(mealPlanDays);
            }
        }

        private static void SeedMealPlanMeals(JasmimDbContext context)
        {
            if (!context.MealPlanDays.Any())
            {
                var mealPlanDays = context.MealPlanDays.ToList();

                // ------------------------
                // REFEIÇÕES (3 por dia)
                // ------------------------
                var meals = new List<MealPlanMeal>();

                foreach (var day in mealPlanDays)
                {
                    meals.AddRange(new[]
                    {
                    // Café da manhã
                    new MealPlanMeal
                    {
                        MealPlanDayId = day.Id,
                        Period = MealPeriodEnum.Breakfast,
                        Time = "07:00",
                        Description = "Ovos mexidos + Aveia com frutas",
                        Quantity = "3 ovos + 50g de aveia + 1 banana",
                        Notes = "Pode substituir banana por maçã",
                        CreatedAt = DateTime.UtcNow
                    },

                    // Almoço
                    new MealPlanMeal
                    {
                        MealPlanDayId = day.Id,
                        Period = MealPeriodEnum.Lunch,
                        Time = "12:30",
                        Description = "Arroz + Feijão + Frango grelhado",
                        Quantity = "150g frango + 100g arroz + 1 concha feijão",
                        Notes = "Adicionar salada verde à vontade",
                        CreatedAt = DateTime.UtcNow
                    },

                    // Jantar
                    new MealPlanMeal
                    {
                        MealPlanDayId = day.Id,
                        Period = MealPeriodEnum.Dinner,
                        Time = "19:30",
                        Description = "Crepioca de frango + chá",
                        Quantity = "2 ovos + 1 colher de tapioca + 80g frango",
                        Notes = "Evitar alimentos pesados após 20h",
                        CreatedAt = DateTime.UtcNow
                    }
                });
                }

                context.MealPlanMeals.AddRange(meals);
            }
        }

    }
}
