using LookClosely_Original.LookCloselyViewModels;

namespace LookClosely_Original.Services.Core.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<ScoreViewModel>> GetTopScoresAsync(int count);
    }
}
