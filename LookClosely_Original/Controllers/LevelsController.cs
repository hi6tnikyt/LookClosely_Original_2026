using LookClosely_Original.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class LevelsController : Controller
{
    private readonly ApplicationDbContext _context;

    public LevelsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var levels = await _context.Levels.ToListAsync();
        return View(levels);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var level = await _context.Levels
            .FirstOrDefaultAsync(m => m.Id == id);

        if (level == null)
        {
            return NotFound();
        }

        return View(level);
    }
}