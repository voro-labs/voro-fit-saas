using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;
using VoroFit.Domain.Entities;
using VoroFit.Domain.Entities.Evolution;
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

            // SEED: Measurements
            SeedMeasurements(context);

            await context.SaveChangesAsync();

            // SEED: Workout Histories
            SeedWorkoutHistories(context);

            await context.SaveChangesAsync();

            // SEED: Workout Exercises
            SeedWorkoutExercises(context);

            await context.SaveChangesAsync();

            // SEED: Meal Plans
            SeedMealPlans(context);

            await context.SaveChangesAsync();

            // SEED: Meal Plan Days
            SeedMealPlanDays(context);

            await context.SaveChangesAsync();

            // SEED: Meal Plan Meals
            SeedMealPlanMeals(context);

            await context.SaveChangesAsync();
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
                    UserName = "admin",
                    NormalizedUserName = "admin".ToUpper(),
                    Email = "contato@vorolabs.app",
                    NormalizedEmail = "contato@vorolabs.app".ToUpper(),
                    FirstName = "System",
                    LastName = "Administrator",
                    CountryCode = "BR",
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
                    UserExtension = new UserExtension
                    {
                        Instances = [
                            new Instance
                            {
                                Name = "voro-evolution"
                            }
                        ]
                    }
                };

                context.Users.Add(admin);

                var trainerRole = context.Roles.FirstOrDefault(r => r.Name == "Trainer");

                var trainer = new User
                {
                    UserName = "trainer",
                    NormalizedUserName = "trainer".ToUpper(),
                    Email = "contato@vorolabs.app",
                    NormalizedEmail = "contato@vorolabs.app".ToUpper(),
                    FirstName = "System",
                    LastName = "Treinador",
                    CountryCode = "BR",
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
                    UserExtension = new UserExtension
                    {
                        Instances = [
                            new Instance
                            {
                                Name = "voro-evolution"
                            }
                        ]
                    }
                };

                context.Users.Add(trainer);

                var studentRole = context.Roles.FirstOrDefault(r => r.Name == "Student");

                var student = new User
                {
                    UserName = "student",
                    NormalizedUserName = "student".ToUpper(),
                    Email = "contato@vorolabs.app",
                    NormalizedEmail = "contato@vorolabs.app".ToUpper(),
                    FirstName = "System",
                    LastName = "Estudante",
                    CountryCode = "BR",
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
                    UserExtension = new UserExtension
                    {
                        Instances = [
                            new Instance
                            {
                                Name = "voro-evolution"
                            }
                        ]
                    }
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

        private static void SeedWorkoutHistories(JasmimDbContext context)
        {
            if (!context.WorkoutHistories.Any())
            {
                var student = context.Students.FirstOrDefault();
                if (student == null) return;

                var histories = new List<WorkoutHistory>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.UserExtensionId,
                        Name = "Treino de Peito e Tríceps",

                        CreatedDate = DateTimeOffset.UtcNow.AddDays(-2),
                        LastUpdated = DateTimeOffset.UtcNow.AddDays(-2),

                        Status = WorkoutStatusEnum.Active,
                        Exercises = [] // preenchido depois no seed de WorkoutExercise
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.UserExtensionId,
                        Name = "Treino de Costas e Bíceps",

                        CreatedDate = DateTimeOffset.UtcNow.AddDays(-1),
                        LastUpdated = DateTimeOffset.UtcNow.AddDays(-1),

                        Status = WorkoutStatusEnum.Active,
                        Exercises = []
                    }
                };

                context.WorkoutHistories.AddRange(histories);
            }
        }

        private static void SeedWorkoutExercises(JasmimDbContext context)
        {
            if (!context.WorkoutExercises.Any())
            {
                var student = context.Users.FirstOrDefault(u => u.UserRoles.Any(ur => ur.RoleId.ToString() == RoleConstant.Student));

                if (student == null) return;

                var histories = context.WorkoutHistories.ToList();
                var exercises = context.Exercises.ToList();

                if (!histories.Any() || !exercises.Any()) return;

                var chestWorkout = histories.FirstOrDefault(h => h.Name.Contains("Peito"));
                var backWorkout = histories.FirstOrDefault(h => h.Name.Contains("Costas"));

                if (chestWorkout == null || backWorkout == null) return;

                // pegar exercícios já criados
                var supino = exercises.FirstOrDefault(e => e.Name == "Supino Reto");
                var agachamento = exercises.FirstOrDefault(e => e.Name == "Agachamento Livre");
                var barraFixa = exercises.FirstOrDefault(e => e.Name == "Barra Fixa");

                // se quiser depois colocamos mais exercícios oficiais
                var workoutExercises = new List<WorkoutExercise>
                {
                    // -----------------------------------------------
                    // TREINO DE PEITO / TRÍCEPS
                    // -----------------------------------------------

                    new()
                    {
                        Id = Guid.NewGuid(),
                        WorkoutHistoryId = chestWorkout.Id,
                        ExerciseId = supino!.Id,
                        StudentId = student.Id,
                        Order = 1,
                        Sets = 3,
                        Reps = 10,
                        RestInSeconds = 90,
                        Weight = 40f
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        WorkoutHistoryId = chestWorkout.Id,
                        ExerciseId = agachamento!.Id,
                        StudentId = student.Id,
                        Order = 2,
                        Sets = 4,
                        Reps = 12,
                        RestInSeconds = 120,
                        Weight = 0f // exercício sem carga
                    },

                    // -----------------------------------------------
                    // TREINO COSTAS / BÍCEPS
                    // -----------------------------------------------

                    new()
                    {
                        Id = Guid.NewGuid(),
                        WorkoutHistoryId = backWorkout.Id,
                        ExerciseId = barraFixa!.Id,
                        StudentId = student.Id,
                        Order = 1,
                        Sets = 3,
                        Reps = 8,
                        RestInSeconds = 120,
                        Weight = null // peso corporal
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        WorkoutHistoryId = backWorkout.Id,
                        ExerciseId = agachamento.Id,
                        StudentId = student.Id,
                        Order = 2,
                        Sets = 4,
                        Reps = 10,
                        RestInSeconds = 120,
                        Weight = 0f
                    }
                };

                context.WorkoutExercises.AddRange(workoutExercises);
            }
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
                    LastUpdated = DateTime.UtcNow,
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
                    "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo"
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
                        Period = "Café da Manhã",
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
                        Period = "Almoço",
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
                        Period = "Jantar",
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
