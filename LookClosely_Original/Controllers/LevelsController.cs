using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LookClosely_Original.Data;
using LookClosely.Models;
using LookClosely_Original.ViewModels;
using Microsoft.AspNetCore.Authorization;
using LookClosely_Original.Services.Core;
using LookClosely_Original.Services.Core.Interfaces;

namespace LookClosely_Original.Controllers
{
    [Authorize]
    public class LevelsController : Controller
    {
        private readonly ILevelService levelService;

        public LevelsController(ILevelService levelService)
        {
            this.levelService = levelService;
        }

        public async Task<IActionResult> Index()
        {
            var levels = await levelService.GetAllLevelsAsync();
            return View(levels);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LevelViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await levelService.CreateLevelAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await levelService.GetLevelByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LevelViewModel model)
        {
            if (id != model.Id || !ModelState.IsValid) return View(model);
            await levelService.EditLevelAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await levelService.GetLevelByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [Authorize]
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