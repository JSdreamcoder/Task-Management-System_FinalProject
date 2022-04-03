using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem_FinalProject.Data;

namespace TaskManagementSystem_FinalProject.Models
{
    public class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = new ApplicationDbContext
                (serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!context.Roles.Any())
            {
                List<string> roles = new List<string>()
                {
                    "ProjectManager","Developer"
                };

                foreach (string role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


            // for seed
            if (!context.Users.Any())
            {
                AppUser user = new AppUser()
                {
                    Email = "projectmanager@mitt.ca",
                    NormalizedEmail = "PROJECTMANAGER@MITT.CA",
                    UserName = "projectmanager@mitt.ca",
                    NormalizedUserName = "PROJECTMANAGER@MITT.CA",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                AppUser user2 = new AppUser()
                {
                    Email = "developer1@mitt.ca",
                    NormalizedEmail = "DEVELOPER1@MITT.CA",
                    UserName = "developer1@mitt.ca",
                    NormalizedUserName = "DEVELOPER1@MITT.CA",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                AppUser user3 = new AppUser()
                {
                    Email = "developer2@mitt.ca",
                    NormalizedEmail = "DEVELOPER2@MITT.CA",
                    UserName = "developer2@mitt.ca",
                    NormalizedUserName = "DEVELOPER2@MITT.CA",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var passwordHasher = new PasswordHasher<AppUser>();
                var hasedPassword = passwordHasher.HashPassword(user, "P@ssword1");
                user.PasswordHash = hasedPassword;
                await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, "ProjectManager");

                var passwordHasher2 = new PasswordHasher<AppUser>();
                var hasedPassword2 = passwordHasher2.HashPassword(user2, "P@ssword1");
                user2.PasswordHash = hasedPassword2;
                await userManager.CreateAsync(user2);
                await userManager.AddToRoleAsync(user2, "Developer");

                var passwordHasher3 = new PasswordHasher<AppUser>();
                var hasedPassword3 = passwordHasher3.HashPassword(user3, "P@ssword1");
                user3.PasswordHash = hasedPassword3;
                await userManager.CreateAsync(user3);
                await userManager.AddToRoleAsync(user3, "Developer");
            }
            context.SaveChanges();  // or await userManger.UpdateAsy(user)  await userManger.UpdateAsy(user2)

            if (!context.Project.Any())
            {
                var project = new Project();
                project.Name = "Proeject1";
                project.Budget = 1000000;
                project.StartDate = new DateTime(2022, 3, 1);
                project.DeadLine = new DateTime(2022, 6, 1);
                context.Project.Add(project);

                var project2 = new Project();
                project2.Name = "Proeject2";
                project2.Budget = 2000000;
                project.StartDate = new DateTime(2022, 2,15);
                project2.DeadLine = new DateTime(2022, 10,15);
                context.Project.Add(project2);

            }
            context.SaveChanges();
        
            if (!context.AppTask.Any())
            {
                var task1 = new AppTask();
                task1.Name = "task1";
                task1.ProjectId = context.Project.First(p => p.Name == "Proeject1").Id;
                task1.CompletePercentage = 30;
                task1.AppUserId = context.AppUser.First(u => u.UserName == "developer1@mitt.ca").Id;
                context.AppTask.Add(task1);

                var task2 = new AppTask();
                task2.Name = "task2";
                task2.ProjectId = context.Project.First(p => p.Name == "Proeject1").Id;
                task2.CompletePercentage = 70;
                task2.AppUserId = context.AppUser.First(u => u.UserName == "developer1@mitt.ca").Id;
                context.AppTask.Add(task2);

                var task3 = new AppTask();
                task3.Name = "task3";
                task3.ProjectId = context.Project.First(p => p.Name == "Proeject1").Id;
                task3.CompletePercentage = 100;
                task3.AppUserId = context.AppUser.First(u => u.UserName == "developer1@mitt.ca").Id;
                context.AppTask.Add(task3);

                var task4 = new AppTask();
                task4.Name = "task4";
                task4.ProjectId = context.Project.First(p => p.Name == "Proeject2").Id;
                task4.CompletePercentage = 10;
                task4.AppUserId = context.AppUser.First(u => u.UserName == "developer2@mitt.ca").Id;
                context.AppTask.Add(task4);

                var task5 = new AppTask();
                task5.Name = "task5";
                task5.ProjectId = context.Project.First(p => p.Name == "Proeject2").Id;
                task5.CompletePercentage = 40;
                task5.AppUserId = context.AppUser.First(u => u.UserName == "developer2@mitt.ca").Id;
                context.AppTask.Add(task5);

                var task6 = new AppTask();
                task6.Name = "task6";
                task6.ProjectId = context.Project.First(p => p.Name == "Proeject2").Id;
                task6.CompletePercentage = 100;
                task6.AppUserId = context.AppUser.First(u => u.UserName == "developer2@mitt.ca").Id;
                context.AppTask.Add(task6);

                var task7 = new AppTask();
                task7.Name = "task7";
                task7.ProjectId = context.Project.First(p => p.Name == "Proeject2").Id;
                task7.CompletePercentage = 70;
                task7.AppUserId = context.AppUser.First(u => u.UserName == "developer2@mitt.ca").Id;
                context.AppTask.Add(task7);
            }

            context.SaveChanges();
        }
    }
}
