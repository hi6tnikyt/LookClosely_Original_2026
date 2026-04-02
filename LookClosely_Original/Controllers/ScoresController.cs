using Microsoft.AspNetCore.Mvc;
using LookClosely_Original.Services.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace LookClosely_Original.Controllers
{
    [Authorize]
    public class ScoresController : Controller
    {
        private readonly IScoreService scoreService;

        public ScoresController(IScoreService scoreService)
        {
            this.scoreService = scoreService;
        }

        public async Task<IActionResult> Leaderboard()
        {
            var topScores = await scoreService.GetTopScoresAsync(10);
            return View(topScores);
        }
    }
}