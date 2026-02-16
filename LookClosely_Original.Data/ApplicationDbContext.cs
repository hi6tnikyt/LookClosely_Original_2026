using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LookClosely.Models;

namespace LookClosely_Original.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Level> Levels { get; set; } = null!;
        public virtual DbSet<Score> Scores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Level>().HasData(
                new Level
                {
                    Id = 1,
                    Name = "Стаята на детектива",
                    Difficulty = "Easy",
                    ImagePath = "/images/levels/level1.webp"
                },
                new Level
                {
                    Id = 2,
                    Name = "Изоставената библиотека",
                    Difficulty = "Medium",
                    ImagePath = "/images/levels/level2.jpg"
                },
                new Level
                {
                    Id = 3,
                    Name = "Тайното мазе",
                    Difficulty = "Hard",
                    ImagePath = "/images/levels/level3.jpg"
                }
            );
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
            });

                modelBuilder.Entity<Score>()
                    .HasOne(s => s.User)
                    .WithMany() // Или .WithMany(u => u.Scores) 
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Score>()
                    .HasOne(s => s.Level)
                    .WithMany()
                    .HasForeignKey(s => s.LevelId)
                    .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}