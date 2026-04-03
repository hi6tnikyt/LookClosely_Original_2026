using System.Linq.Expressions;
using LookClosely_Original.Data.Models;
using LookClosely_Original.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LookClosely_Original.Data.Repository
{
    public class LevelRepository : BaseRepository, ILevelRepository
    {
        public LevelRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Level>> GetAllLevelsAsync(
            Expression<Func<Level, bool>>? filterQuery = null,
            Expression<Func<Level, Level>>? projectionQuery = null)
        {
            IQueryable<Level> query = DbContext.Levels.AsNoTracking();

            if (filterQuery != null)
            {
                query = query.Where(filterQuery);
            }

            if (projectionQuery != null)
            {
                query = query.Select(projectionQuery);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Level?> GetLevelByIdAsync(int id)
        {
            return await DbContext.Levels
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(Level level)
        {
            await DbContext.Levels.AddAsync(level);
            await SaveChangeAsync();
        }

        public new async Task<int> SaveChangeAsync()
        {
            return await base.SaveChangeAsync();
        }
    }
}