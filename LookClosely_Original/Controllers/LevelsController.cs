using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LookClosely_Original.Data;
using LookClosely.Models;
using LookClosely_Original.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LookClosely_Original.Controllers
{
    [Authorize]
    public class LevelsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LevelsController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            List<LevelViewModel> levels = await dbContext
                .Levels
                .Select(l => new LevelViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    ImagePath = l.ImagePath!,
                    Difficulty = l.Difficulty
                }).ToListAsync();

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
            if (!ModelState.IsValid)
            {
                return View(model);
            } 

            Level? level = new Level
            {
                Name = model.Name,
                ImagePath = model.ImagePath,
                Difficulty = model.Difficulty!
            };

            dbContext.Levels.Add(level);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Level? level = await dbContext.Levels.FindAsync(id);
            if (level == null)
            {
                return NotFound();
            }

            LevelViewModel? model = new LevelViewModel
            {
                Id = level.Id,
                Name = level.Name,
                ImagePath = level.ImagePath!
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LevelViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Level? level = await dbContext.Levels.FindAsync(id);
            if (level == null)
            {
                return NotFound();
            }

            level.Name = model.Name;
            level.ImagePath = model.ImagePath;
            level.Difficulty = model.Difficulty!;

            dbContext.Update(level);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Level? level = await dbContext
                .Levels
                .FirstOrDefaultAsync(m => m.Id == id);

            if (level == null)
            {
                return NotFound();
            }

            LevelViewModel? model = new LevelViewModel
            {
                Id = level.Id,
                Name = level.Name,
                ImagePath = level.ImagePath!,
                Difficulty = level.Difficulty
            };

            return View(model); 
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Level? level = await dbContext
                .Levels.
                FindAsync(id);

            if (level != null)
            {
                dbContext.Levels.Remove(level);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Level? level = await dbContext
                .Levels
                .FindAsync(id);

            if (level == null)
            {
                return NotFound();
            }

            LevelViewModel? model = new LevelViewModel
            {
                Id = level.Id,
                Name = level.Name,
                ImagePath = level.ImagePath!,
                Difficulty = level.Difficulty
            };

            return View(model);
        }
    }
}