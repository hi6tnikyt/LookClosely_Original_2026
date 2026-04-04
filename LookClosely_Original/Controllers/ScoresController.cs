using Microsoft.AspNetCore.Mvc;
using LookClosely_Original.Services.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using LookClosely_Original.LookCloselyViewModels;


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

        public async Task<IActionResult> Leaderboard(int page = 1, string? searchTerm = null)
        {
            const int pageSize = 10;

            int totalScores = await scoreService.GetScoresCountAsync(searchTerm);
            var scores = await scoreService.GetPagedLeaderboardAsync(page, pageSize, searchTerm);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalScores / (double)pageSize);
            ViewBag.SearchTerm = searchTerm;

            return View(scores);
        }
    }
}