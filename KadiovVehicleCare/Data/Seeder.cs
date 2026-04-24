using Microsoft.AspNetCore.Identity;

namespace KadiovVehicleCare.Data
{
    public class Seeder
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

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
        }
    }
}
