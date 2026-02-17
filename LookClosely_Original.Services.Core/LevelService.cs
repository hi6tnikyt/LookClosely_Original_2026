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
                .Where(l => !l.IsDeleted)
                .AsNoTracking() 
                .Select(l => new LevelViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    ImagePath = l.ImagePath!,
                    Difficulty = l.Difficulty
                })
                .OrderBy(l => l.Difficulty) 
                .ToListAsync();
        }

        public async Task CreateLevelAsync(LevelViewModel model)
        {
            bool exists = await dbContext.Levels.AnyAsync(l => l.Name == model.Name);
            if (exists)
            {
                throw new InvalidOperationException("Ниво с това име вече съществува.");
            }

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
            Level? level = await dbContext.Levels
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);

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

            if (level == null)
            {
                throw new KeyNotFoundException($"Ниво с ID {model.Id} не беше намерено.");
            }

            if (level.Name != model.Name)
            {
                bool nameTaken = await dbContext.Levels.AnyAsync(l => l.Name == model.Name && l.Id != model.Id);
                if (nameTaken)
                {
                    throw new InvalidOperationException("Името вече се ползва от друго ниво.");
                } 
            }

            level.Name = model.Name;
            level.ImagePath = model.ImagePath;
            level.Difficulty = model.Difficulty!;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteLevelAsync(int id)
        {
            var level = await dbContext.Levels
                .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);

            if (level == null)
            {
                throw new ArgumentException("Нивото вече не съществува.");
            }

            level.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await dbContext.Levels.AnyAsync(l => l.Id == id && !l.IsDeleted);
        }
    }
}