using LookClosely_Original.LookCloselyViewModels;

namespace LookClosely_Original.Services.Core.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<ScoreViewModel>> GetPagedLeaderboardAsync(int page = 1, int pageSize = 10, string? searchTerm = null);
        Task AddScoreAsync(int levelId, string userId, int points, int timeInSeconds);
        Task<IEnumerable<ScoreViewModel>> GetTopScoresAsync(int count);
        Task<int> GetScoresCountAsync(string? searchTerm = null);
    } 
}
