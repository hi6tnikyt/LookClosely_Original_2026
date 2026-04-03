using LookClosely_Original.Data.Repository.Contracts;
using LookClosely_Original.Data.Repository;
using LookClosely_Original.Data;
using Microsoft.EntityFrameworkCore;
using LookClosely_Original.Data.Models;

public class ScoreRepository : BaseRepository, IScoreRepository
{
    public ScoreRepository(ApplicationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<IEnumerable<Score>> GetAllScoresAsync()
    {
        return await DbContext.Scores
            .Include(s => s.User)
            .Include(s => s.Level)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Score?> GetScoreByUserAndLevelAsync(string userId, int levelId)
    {
        return await this.DbContext.Scores
            .FirstOrDefaultAsync(s => s.UserId == userId && s.LevelId == levelId);
    }

    public IQueryable<Score> GetAllScoresQuery()
    {
        return this.DbContext.Scores
         .Include(s => s.User);
    }

    public async Task<bool> UpdateScoreAsync(Score score)
    {
        this.DbContext.Scores.Update(score);
        int result = await this.DbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task AddScoreAsync(Score score)
    {
        await DbContext.Scores.AddAsync(score);
        await DbContext.SaveChangesAsync();
    }

    public new async Task<int> SaveChangeAsync() => await base.SaveChangeAsync();
}
