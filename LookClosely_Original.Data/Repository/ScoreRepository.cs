using LookClosely_Original.Data.Repository.Contracts;
using LookClosely_Original.Data.Repository;
using LookClosely_Original.Data;
using Microsoft.EntityFrameworkCore;
using LookClosely_Original.Data.Models;

public class ScoreRepository : BaseRepository, IScoreRepository
{
    public ScoreRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<Score>> GetAllScoresAsync()
    {
        return await DbContext.Scores
            .Include(s => s.User)
            .Include(s => s.Level)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddScoreAsync(Score score)
    {
        await DbContext.Scores.AddAsync(score);
    }

    public new async Task<int> SaveChangeAsync() => await base.SaveChangeAsync();
}
