using Microsoft.AspNetCore.Mvc;
using LookClosely_Original.ViewModels;
using Microsoft.AspNetCore.Authorization;
using LookClosely_Original.Services.Core.Interfaces;
using System.Security.Claims;


namespace LookClosely_Original.Controllers
{
    [Authorize]
    public class LevelsController : Controller
    {
        private readonly ILevelService levelService;
        private readonly IScoreService scoreService;

        public LevelsController(ILevelService levelService, IScoreService scoreService)
        {
            this.levelService = levelService;
            this.scoreService = scoreService;
        }

        public async Task<IActionResult> Index()
        {
            var levels = await levelService.GetAllLevelsAsync();
            return View(levels);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LevelViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await levelService.CreateLevelAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await levelService.GetLevelByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LevelViewModel model)
        {
            if (id != model.Id || !ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                await levelService.EditLevelAsync(model, userId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckClick(int levelId, double x, double y)
        {
            bool isHit = await levelService.CheckHitAsync(levelId, x, y);

            if (isHit)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                await scoreService.AddScoreAsync(levelId, userId, 100);

                return Json(new { success = true, message = "Поздравления! Намери обекта!" });
            }

            return Json(new { success = false, message = " Опитай пак!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await levelService.GetLevelByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await levelService.DeleteLevelAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await levelService.GetLevelByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}