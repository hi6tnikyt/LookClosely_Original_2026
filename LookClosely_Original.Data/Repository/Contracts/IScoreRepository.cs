
using LookClosely_Original.Data.Models;

namespace LookClosely_Original.Data.Repository.Contracts
{
    public interface IScoreRepository
    {
        Task AddScoreAsync(Score score);
        Task<int> SaveChangeAsync();
        Task<Score?> GetScoreByUserAndLevelAsync(string userId, int levelId);
       IQueryable<Score> GetAllScoresQuery();
        Task<bool> UpdateScoreAsync(Score score);
    }
}
