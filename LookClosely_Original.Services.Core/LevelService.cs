using LookClosely_Original.Data;
using LookClosely_Original.Services.Core.Interfaces;
using LookClosely_Original.ViewModels;
using Microsoft.EntityFrameworkCore;
using LookClosely.Models;

namespace LookClosely_Original.Services.Core
{
    public class LevelService : ILevelService
    {
        private readonly ApplicationDbContext dbContext;

        public LevelService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<LevelViewModel>> GetAllLevelsAsync()
        {
            return await dbContext.Levels
                .Select(l => new LevelViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    ImagePath = l.ImagePath!,
                    Difficulty = l.Difficulty
                }).ToListAsync();
        }

        public async Task CreateLevelAsync(LevelViewModel model)
        {
            Level? level = new Level
            {
                Name = model.Name,
                ImagePath = model.ImagePath,
                Difficulty = model.Difficulty!
            };

            await dbContext.Levels.AddAsync(level);
            await dbContext.SaveChangesAsync();
        }

        public async Task<LevelViewModel?> GetLevelByIdAsync(int id)
        {
            Level? level = await dbContext.Levels.FindAsync(id);
            if (level == null) return null;

            return new LevelViewModel
            {
                Id = level.Id,
                Name = level.Name,
                ImagePath = level.ImagePath!,
                Difficulty = level.Difficulty
            };
        }

        public async Task EditLevelAsync(LevelViewModel model)
        {
            Level? level = await dbContext.Levels.FindAsync(model.Id);
            if (level != null)
            {
                level.Name = model.Name;
                level.ImagePath = model.ImagePath;
                level.Difficulty = model.Difficulty!;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteLevelAsync(int id)
        {
            Level? level = await dbContext.Levels.FindAsync(id);
            if (level != null)
            {
                dbContext.Levels.Remove(level);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}