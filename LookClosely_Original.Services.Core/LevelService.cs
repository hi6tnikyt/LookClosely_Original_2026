using LookClosely_Original.Data.Models;
using LookClosely_Original.Data;
using LookClosely_Original.Services.Core.Interfaces;
using LookClosely_Original.ViewModels;
using Microsoft.EntityFrameworkCore;
using static LookClosely_Original.GCommon.Exceptions.ErrorMessages;

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
                    Difficulty = l.Difficulty,

                    TargetX = l.TargetX,
                    TargetY = l.TargetY,
                    TargetRadius = l.TargetRadius
                })
                .OrderBy(l => l.Difficulty)
                .ThenBy(l => l.Name)
                .ToListAsync();
        }

        public async Task CreateLevelAsync(LevelViewModel model)
        {
            bool exists = await dbContext
                .Levels
                .AnyAsync(l => l.Name == model.Name);
            if (exists)
            {
                throw new InvalidOperationException("Ниво с това име вече съществува.");
            }

            Level level = new Level
            {
                Name = model.Name,
                ImagePath = model.ImagePath,
                Difficulty = model.Difficulty!,

                TargetX = model.TargetX,
                TargetY = model.TargetY,
                TargetRadius = model.TargetRadius
            };

            await dbContext.Levels.AddAsync(level);
            await dbContext.SaveChangesAsync();
        }

        public async Task<LevelViewModel?> GetLevelByIdAsync(int id)
        {
            Level? level = await dbContext
                .Levels
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (level == null) return null;

            return new LevelViewModel
            {
                Id = level.Id,
                Name = level.Name,
                ImagePath = level.ImagePath!,
                Difficulty = level.Difficulty,

                TargetX = level.TargetX,
                TargetY = level.TargetY,
                TargetRadius = level.TargetRadius
            };
        }

        public async Task EditLevelAsync(LevelViewModel model, string userId)
        {
            Level? level = await dbContext
                .Levels
                .FindAsync(model.Id);

            if (level == null || level.IsDeleted)
            {
                throw new KeyNotFoundException(LevelNotFound); 
            }

            try
            {
                level.Name = model.Name;
                level.ImagePath = model.ImagePath;
                level.Difficulty = model.Difficulty!;

                level.TargetX = model.TargetX;
                level.TargetY = model.TargetY;
                level.TargetRadius = model.TargetRadius;

                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(UnexpectedError, ex); 
            }
        }

        public async Task DeleteLevelAsync(int id)
        {
            Level? level = await dbContext.Levels
                .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);

            if (level == null)
            {
                throw new ArgumentException(LevelNotFound);
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