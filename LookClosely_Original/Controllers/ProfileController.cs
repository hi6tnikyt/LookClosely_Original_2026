using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LookClosely.Models;
using System.Security.Claims;
using LookClosely_Original.Data;
using LookClosely_Original.LookCloselyViewModels.Event;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ApplicationDbContext dbContext;

    public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        this.userManager = userManager;
        this.dbContext = context;
    }

    public async Task<IActionResult> Index()
    {
        String? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        ApplicationUser? user = await userManager
            .Users
            .Include(u => u.Scores)
            .ThenInclude(s => s.Level)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return NotFound();

        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        String? userId = userManager.GetUserId(User);
        ApplicationUser? user = await userManager
            .FindByIdAsync(userId!);

        if (user == null)
        {
            return NotFound();
        }

        EditProfileViewModel? model = new EditProfileViewModel
        {
            Bio = user.Bio,
            CurrentAvatarPath = user.AvatarPath
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProfileViewModel model)
    {
        ApplicationUser? user = await userManager
            .GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (model.AvatarFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.AvatarFile.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/avatars", fileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await model.AvatarFile.CopyToAsync(stream);
                }

                user.AvatarPath = "/images/avatars/" + fileName;
            }

            user.Bio = model.Bio;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
        }

        return View(model);
    }
}
