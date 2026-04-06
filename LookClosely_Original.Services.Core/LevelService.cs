using LookClosely_Original.Data.Models;
using LookClosely_Original.Services.Core.Interfaces;
using LookClosely_Original.ViewModels;
using LookClosely_Original.Data.Repository.Contracts;
using static LookClosely_Original.GCommon.Exceptions.ErrorMessages;
using LookClosely_Original.GCommon.Exceptions;



namespace LookClosely_Original.Services.Core
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository levelRepository;
        private readonly IScoreService scoreService;

        public LevelService(ILevelRepository levelRepository, IScoreService scoreService)
        {
            this.levelRepository = levelRepository;
            this.scoreService = scoreService;
        }

        public async Task<IEnumerable<LevelViewModel>> GetAllLevelsAsync()
        {
            IEnumerable<Level> levels = await levelRepository.GetAllLevelsAsync(l => !l.IsDeleted);

            return levels
                .Select(l => new LevelViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    ImagePath = l.ImagePath!,
                    Difficulty = l.Difficulty,
                    TargetObjectName = l.TargetObjectName,
                    TargetX = l.TargetX,
                    TargetY = l.TargetY,
                    TargetRadius = l.TargetRadius
                })
                .OrderBy(l => l.Difficulty)
                .ThenBy(l => l.Name)
                .ToList();
        }

        public async Task<LevelViewModel?> GetLevelByIdAsync(int id)
        {
            Level? level = await levelRepository.GetLevelByIdAsync(id);

            if (level == null || level.IsDeleted)
            {
                throw new EntityNotFoundException();
            } 

            return new LevelViewModel
            {
                Id = level.Id,
                Name = level.Name,
                ImagePath = level.ImagePath!,
                Difficulty = level.Difficulty,
                TargetObjectName = level.TargetObjectName,
                TargetX = level.TargetX,
                TargetY = level.TargetY,
                TargetRadius = level.TargetRadius
            };
        }

        public async Task CreateLevelAsync(LevelViewModel model)
        {
            IEnumerable<Level> existing = await levelRepository.GetAllLevelsAsync(l => l.Name == model.Name);
            if (existing.Any())
            {
                throw new EntityInputDataException(LevelNameAlreadyExists);
            }

            Level level = new Level
            {
                Name = model.Name,
                ImagePath = model.ImagePath,
                Difficulty = model.Difficulty!,
                TargetObjectName = model.TargetObjectName,
                TargetX = model.TargetX,
                TargetY = model.TargetY,
                TargetRadius = model.TargetRadius
            };

            await levelRepository.AddAsync(level);
        }

        public async Task<bool> CheckClickAsync(int levelId, double x, double y, int timeInSeconds, string userId)
        {
            Level? level = await levelRepository.GetLevelByIdAsync(levelId);

            if (level == null || level.IsDeleted)
            {
                throw new EntityNotFoundException();
            }

            if (timeInSeconds < 0)
            {
                throw new EntityInputDataException(InvalidTime);
            }

            double distance = Math.Sqrt(Math.Pow(x - level.TargetX, 2) + Math.Pow(y - level.TargetY, 2));

            if (distance <= level.TargetRadius)
            {
                await scoreService.AddScoreAsync(levelId, userId, 100, timeInSeconds);
                return true;
            }

            return false;
        }

        public async Task<bool> CheckHitAsync(int levelId, double x, double y)
        {
            Level? level = await levelRepository.GetLevelByIdAsync(levelId);
            if (level == null || level.IsDeleted)
            {
                return false;
            }

            double distance = Math.Sqrt(Math.Pow(x - level.TargetX, 2) + Math.Pow(y - level.TargetY, 2));
            return distance <= level.TargetRadius;
        }

        public async Task EditLevelAsync(LevelViewModel model, string userId)
        {
            Level? level = await levelRepository.GetLevelByIdAsync(model.Id);

            if (level == null || level.IsDeleted)
            {
                throw new EntityNotFoundException();
            }

            level.Name = model.Name;
            level.ImagePath = model.ImagePath;
            level.Difficulty = model.Difficulty!;
            level.TargetObjectName = model.TargetObjectName;
            level.TargetX = model.TargetX;
            level.TargetY = model.TargetY;
            level.TargetRadius = model.TargetRadius;

            await levelRepository.UpdateAsync(level);
            await levelRepository.SaveChangeAsync();
        }

        public async Task DeleteLevelAsync(int id)
        {
            Level? level = await levelRepository.GetLevelByIdAsync(id);

            if (level == null || level.IsDeleted) 
            {
                throw new EntityNotFoundException();
            }

            level.IsDeleted = true;

            await levelRepository.UpdateAsync(level);
            await levelRepository.SaveChangeAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            IEnumerable<Level> levels = await levelRepository.GetAllLevelsAsync(l => l.Id == id && !l.IsDeleted);
            return levels.Any();
        }

        public int CalculateScore(int timeInSeconds)
        {
            int points = 100 - Math.Max(0, timeInSeconds - 10);

            return Math.Max(10, points);
        }
    }
}