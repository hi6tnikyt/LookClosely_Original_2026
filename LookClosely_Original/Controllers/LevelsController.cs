using Microsoft.AspNetCore.Mvc;
using LookClosely_Original.ViewModels;
using Microsoft.AspNetCore.Authorization;
using LookClosely_Original.Services.Core.Interfaces;
using System.Security.Claims;
using LookClosely_Original.GCommon.Exceptions;


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

            try
            {
                await levelService.CreateLevelAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityInputDataException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await levelService.GetLevelByIdAsync(id);
                return View(model);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
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
            catch (EntityEditPersistFailException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckClick(int levelId, double x, double y, int timeInSeconds)
        {
            try
            {
                bool isHit = await levelService.CheckHitAsync(levelId, x, y);
                if (isHit)
                {
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                    await scoreService.AddScoreAsync(levelId, userId, 100, timeInSeconds);
                    return Json(new { success = true, message = "Поздравления! Намери обекта!" });
                }
                return Json(new { success = false, message = "Опитай пак!" });
            }
            catch (Exception ex) 
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                LevelViewModel? model = await levelService.GetLevelByIdAsync(id);
                return View(model);
            }
            catch (EntityNotFoundException)
            {
                return NotFound(); 
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await levelService.DeleteLevelAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (EntityEditPersistFailException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                LevelViewModel? model = await levelService.GetLevelByIdAsync(id);
                return View(model);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}