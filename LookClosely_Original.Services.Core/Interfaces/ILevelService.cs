using LookClosely_Original.ViewModels;

namespace LookClosely_Original.Services.Core.Interfaces
{
    public interface ILevelService
    {
        Task<IEnumerable<LevelViewModel>> GetAllLevelsAsync();
        Task CreateLevelAsync(LevelViewModel model);
        Task<LevelViewModel?> GetLevelByIdAsync(int id);
        Task EditLevelAsync(LevelViewModel model, string Id);
        Task DeleteLevelAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> CheckHitAsync(int levelId, double x, double y);
        Task<bool> CheckClickAsync(int levelId, double x, double y, int timeInSeconds, string userId);
    }
}
