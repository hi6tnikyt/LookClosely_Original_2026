using LookClosely.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LookClosely_Original.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Level> Levels { get; set; } = null!;
        public DbSet<Score> Scores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Level>().HasData(
                new Level
                {
                    Id = 1,
                    Name = "Стаята на детектива",
                    Difficulty = "Easy",
                    ImagePath = "/images/levels/level1.jpg"
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
        }
    }
}