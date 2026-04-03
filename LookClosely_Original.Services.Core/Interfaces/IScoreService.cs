using LookClosely_Original.LookCloselyViewModels;

namespace LookClosely_Original.Services.Core.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<ScoreViewModel>> GetTopScoresAsync(int count);
        Task<IEnumerable<ScoreViewModel>> GetLeaderboardAsync();
        Task AddScoreAsync(int levelId, string userId, int points, int timeInSeconds);
    }
}
