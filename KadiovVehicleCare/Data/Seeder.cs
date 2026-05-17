using KadiovVehicleCare.Models;
using Microsoft.AspNetCore.Identity;

namespace KadiovVehicleCare.Data
{
    public class Seeder
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            string adminEmail = "admin@autocare.com";
            string adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {

                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                }
            }
            string appUserEmail = "user@autocare.com";
            string userPassword = "User123!";

            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            if (appUser == null)
            {
                appUser = new IdentityUser()
                {
                    UserName = appUserEmail,
                    Email = appUserEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(appUser, userPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, UserRoles.User);
                }

            }
            if (!context.Services.Any())
            {
                var services = new List<Service>
                {
                new Service
                {
                    Name = "Външно измиване",
                    Description = "Основно външно почистване на автомобила.",
                    Price = 15,
                    DurationInMinutes = 30
                },
                new Service
                {
                    Name = "Вътрешно почистване",
                    Description = "Почистване на интериора и прахосмукиране.",
                    Price = 25,
                    DurationInMinutes = 45
                },
                new Service
                {
                    Name = "Пълно почистване",
                    Description = "Комбинирано външно и вътрешно почистване.",
                    Price = 40,
                    DurationInMinutes = 90
                },
                new Service
                {
                    Name = "Полиране",
                    Description = "Полиране на външната повърхност на автомобила.",
                    Price = 60,
                    DurationInMinutes = 120
                },
                new Service
                {
                    Name = "Пране на тапицерия",
                    Description = "Почистване и изпиране на седалки и тапицерия.",
                    Price = 50,
                    DurationInMinutes = 100
                }
            };

                context.Services.AddRange(services);
                await context.SaveChangesAsync();
            }

        }

    }
}
