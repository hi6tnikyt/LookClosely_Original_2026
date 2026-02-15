using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LookClosely_Original.Data;
using LookClosely_Original.LookCloselyViewModels;


namespace LookClosely_Original.Controllers
{
    public class ScoresController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ScoresController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> Leaderboard()
        {
            var topScores = await dbContext
                .Scores
                .Include(s => s.User)
                .Include(s => s.Level)
                .OrderByDescending(s => s.Points)
                .Take(10)
                .Select(s => new ScoreViewModel
                {
                    UserName = s.User.UserName!,
                    LevelName = s.Level.Name,
                    Points = s.Points,
                    DateTime = s.DateTime
                })
                .ToListAsync();

            return View(topScores);
        }
    }
}