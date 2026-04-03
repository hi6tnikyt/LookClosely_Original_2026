using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using LookClosely_Original.Data.Models;
using LookClosely_Original.Data.Repository.Contracts;
using LookClosely_Original.Data.Seeding.Contracts;

namespace LookClosely_Original.Data.Seeding
{
    public class DbSeeder : IDbSeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            ILevelRepository levelRepository = serviceProvider.GetRequiredService<ILevelRepository>();

            await SeedRolesAsync(roleManager);
            await SeedAdminAsync(userManager);
            await SeedLevelsAsync(levelRepository);
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

        private async Task SeedLevelsAsync(ILevelRepository levelRepository)
        {
            var existingLevels = await levelRepository.GetAllLevelsAsync();
            if (existingLevels.Any())
            {
                return;
            }

            var levels = new List<Level>
            {
                new Level
                {
                    Name = "Мистериозната гора",
                    ImagePath = "/images/levels/level1.webp",
                    Difficulty = "Easy",
                    TargetObjectName = "Гъба",
                    TargetX = 450.5,
                    TargetY = 320.0,
                    TargetRadius = 30,
                    IsDeleted = false
                },
                new Level
                {
                    Name = "Старият таван",
                    ImagePath = "/images/levels/level2.jpg",
                    Difficulty = "Medium",
                    TargetObjectName = "Ключ",
                    TargetX = 120.0,
                    TargetY = 580.4,
                    TargetRadius = 20,
                    IsDeleted = false
                },
                  new Level
                {
                    Name = "Изоставената лаборатория",
                    ImagePath = "/images/levels/level3.jpg",
                    Difficulty = "Hard",
                    TargetObjectName = "Микроскоп",
                    TargetX = 250.0,
                    TargetY = 400.0,
                    TargetRadius = 15,
                    IsDeleted = false
                }
              };

            foreach (var level in levels)
            {
                await levelRepository.AddAsync(level);
            }

            await levelRepository.SaveChangeAsync();
        }
    }
}