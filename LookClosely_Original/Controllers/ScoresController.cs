using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LookClosely_Original.Data;
using LookClosely_Original.LookCloselyViewModels.Event;

public class ScoresController : Controller
{
    private readonly ApplicationDbContext _context;

    public ScoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Leaderboard()
    {
        var topScores = await _context.Scores
            .Include(s => s.User)
            .Include(s => s.Level)
            .OrderByDescending(s => s.Points)
            .Take(10)
            .Select(s => new ScoreViewModel 
            {
                UserName = s.User.UserName!,
                LevelName = s.Level.Name,
                Points = s.Points,
                DateAchieved = s.DateTime.ToString("dd.MM.yyyy")
            })
            .ToListAsync();

        return View(topScores);
    }
}