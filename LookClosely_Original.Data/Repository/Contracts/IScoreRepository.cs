
using LookClosely_Original.Data.Models;

namespace LookClosely_Original.Data.Repository.Contracts
{
    public interface IScoreRepository
    {
        Task<IEnumerable<Score>> GetAllScoresAsync();
        Task AddScoreAsync(Score score);
        Task<int> SaveChangeAsync();
    }
}
