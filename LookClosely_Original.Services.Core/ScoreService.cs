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
                .Take(count)
                .Select(s => new ScoreViewModel
                {
                    UserName = s.User.UserName!,
                    LevelName = s.Level.Name,
                    Points = s.Points,
                    DateTime = s.DateTime
                })
                .ToListAsync();

            return topScores;
        }

        public async Task AddScoreAsync(int levelId, string userId, int points)
        {
            var score = new Score
            {
                LevelId = levelId,
                UserId = userId,
                Points = points,
                DateTime = DateTime.Now
            };

            await dbContext.Scores.AddAsync(score);
            await dbContext.SaveChangesAsync();
        }
    }
}
