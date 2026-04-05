using System.Linq.Expressions;
using LookClosely_Original.Data.Models;

namespace LookClosely_Original.Data.Repository.Contracts
{
    public interface ILevelRepository
    {
        Task<IEnumerable<Level>> GetAllLevelsAsync(
        Expression<Func<Level, bool>>? filterQuery = null,
        Expression<Func<Level, Level>>? projectionQuery = null);
        Task<Level?> GetLevelByIdAsync(int id);
        Task AddAsync(Level level);
        Task UpdateAsync(Level level);
        Task<int> SaveChangeAsync();
    }
}
