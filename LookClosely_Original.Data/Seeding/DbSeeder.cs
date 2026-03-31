using LookClosely.Models;
using LookClosely_Original.Data.Seeding.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LookClosely_Original.Data.Seeding
{
    public class DbSeeder : IDbSeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRolesAsync(roleManager);

            await SeedAdminAsync(userManager);
        }

        private async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Administrator", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            string adminEmail = "admin@lookclosely.com";
            ApplicationUser? admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Bio = "System Administrator",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "AdMiN_1-2-3!");
                await userManager.AddToRoleAsync(admin, "Administrator");
            }
        }
    }
}