using Microsoft.EntityFrameworkCore;
using LookClosely_Original.Services.Core.Interfaces;
using LookClosely_Original.LookCloselyViewModels;
using LookClosely_Original.Data.Models;
using LookClosely_Original.Data.Repository.Contracts;
using LookClosely_Original.GCommon.Exceptions;

namespace LookClosely_Original.Services.Core
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreRepository scoreRepository; 

        public ScoreService(IScoreRepository scoreRepository)
        {
            this.scoreRepository = scoreRepository;
        }

        public async Task<IEnumerable<ScoreViewModel>> GetTopScoresAsync(int count)
        {
            IQueryable<Score> scoresQuery =  this.scoreRepository.GetAllScoresQuery();
            return await scoresQuery
              .OrderByDescending(s => s.Points)
              .ThenBy(s => s.TimeInSeconds)
              .Take(count)
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

        public async Task AddScoreAsync(int levelId, string userId, int points, int timeInSeconds)
        {
            Score? existingScore = await this.scoreRepository.GetScoreByUserAndLevelAsync(userId, levelId);

            if (existingScore == null)
            {
                Score newScore = new Score()
                {
                    LevelId = levelId,
                    UserId = userId,
                    Points = points,
                    TimeInSeconds = timeInSeconds,
                    DateTime = DateTime.Now
                };

                await this.scoreRepository.AddScoreAsync(newScore); 
            }
            else
            {
                if (timeInSeconds < existingScore.TimeInSeconds)
                {
                    existingScore.TimeInSeconds = timeInSeconds;
                    existingScore.Points = points; 
                    existingScore.DateTime = DateTime.Now;

                    bool success = await this.scoreRepository.UpdateScoreAsync(existingScore);

                    if (!success)
                    {
                        throw new EntityEditPersistFailException();
                    }
                }
            }
        }

        public async Task<IEnumerable<ScoreViewModel>> GetPagedLeaderboardAsync(int page = 1, int pageSize = 10, string? searchTerm = null)
        {
            IQueryable<Score> scoresQuery = this.scoreRepository.GetAllScoresQuery();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                scoresQuery = scoresQuery.Where(s => s.User.UserName!.Contains(searchTerm));
            }

            return await scoresQuery
                .OrderByDescending(s => s.Points)
                .ThenBy(s => s.TimeInSeconds)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
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

        public async Task<int> GetScoresCountAsync(string? searchTerm = null)
        {
            IQueryable<Score> query = this.scoreRepository.GetAllScoresQuery();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s => s.User.UserName!.Contains(searchTerm));
            }

            return await query.CountAsync();
        }
    }
}
