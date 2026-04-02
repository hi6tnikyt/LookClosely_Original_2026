
using LookClosely_Original.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LookClosely_Original.Data.Configurations
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> entity)
        {
            List<Level> levels = new List<Level>
            {
                new Level
                {
                    Id = 1,
                    Name = "Стаята на детектива",
                    Difficulty = "Easy",
                    ImagePath = "/images/levels/level1.webp",
                    TargetObjectName = "Лупа",
                    TargetX = 18.98, TargetY = 56.06, TargetRadius = 5.0
                },
                new Level
                {
                    Id = 2,
                    Name = "Изоставената библиотека",
                    Difficulty = "Medium",
                    ImagePath = "/images/levels/level2.jpg",
                    TargetObjectName = "Стара книга",
                    TargetX = 30.0, TargetY = 40.0, TargetRadius = 5.0
                },
                new Level
                {
                    Id = 3,
                    Name = "Тайното мазе",
                    Difficulty = "Hard",
                    ImagePath = "/images/levels/level3.jpg",
                    TargetObjectName = "Златен ключ",
                    TargetX = 70.0, TargetY = 20.0, TargetRadius = 5.0
                }
            };

            entity.HasData(levels.ToArray());

            // Може да добавя още нива(10)!
        }
    }
}
