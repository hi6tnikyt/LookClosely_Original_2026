using Microsoft.EntityFrameworkCore;
using LookClosely_Original.Data;
using LookClosely_Original.Services.Core.Interfaces;
using LookClosely_Original.LookCloselyViewModels;
using LookClosely_Original.Data.Models;

namespace LookClosely_Original.Services.Core
{
    public class ScoreService : IScoreService
    {
        private readonly ApplicationDbContext dbContext;

        public ScoreService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ScoreViewModel>> GetTopScoresAsync(int count)
        {
            List<ScoreViewModel> topScores = await dbContext.Scores
                .Include(s => s.User)
                .Include(s => s.Level)
                .OrderByDescending(s => s.Points)
                .ThenBy(s => s.TimeInSeconds)
                .Take(count)
                .Select(s => new ScoreViewModel
                {
                    UserName = s.User.UserName!,
                    LevelName = s.Level.Name,
                    Points = s.Points,
                    TimeInSeconds = s.TimeInSeconds,
                    DateTime = s.DateTime
                })
                .ToListAsync();

            return topScores;
        }

        public async Task AddScoreAsync(int levelId, string userId, int points, int timeInSeconds)
        {
            bool alreadySolved = await dbContext.Scores
                .AnyAsync(s => s.LevelId == levelId && s.UserId == userId);

            if (!alreadySolved)
            {
                var score = new Score
                {
                    LevelId = levelId,
                    UserId = userId,
                    Points = points,
                    TimeInSeconds = timeInSeconds, 
                    DateTime = DateTime.Now
                };

                await dbContext.Scores.AddAsync(score);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ScoreViewModel>> GetLeaderboardAsync()
        {
            return await dbContext.Scores
                .Include(s => s.User)
                .Include(s => s.Level)
                .OrderByDescending(s => s.Points)
                .ThenBy(s => s.TimeInSeconds)
                .Select(s => new ScoreViewModel
                {
                    UserName = s.User.UserName ?? "Анонимен",
                    LevelName = s.Level.Name,
                    Points = s.Points,
                    TimeInSeconds = s.TimeInSeconds,
                    DateTime = s.DateTime
                })
                .ToListAsync();
        }
    }
}
