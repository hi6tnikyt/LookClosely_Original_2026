using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LookClosely.Models;
using System.Security.Claims;
using LookClosely_Original.Data;
using LookClosely_Original.LookCloselyViewModels.Event;
using LookClosely_Original.Services.Core.Interfaces;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUserService userService;
    private readonly UserManager<ApplicationUser> userManager;

    public ProfileController(IUserService userService, UserManager<ApplicationUser> userManager)
    {
        this.userService = userService;
        this.userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        string userId = userManager.GetUserId(User)!;
        ApplicationUser? user = await userService.GetUserProfileAsync(userId);

        if (user == null) return NotFound();

        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        string userId = userManager.GetUserId(User)!;
        ApplicationUser? user = await userManager.FindByIdAsync(userId);

        if (user == null) return NotFound();

        EditProfileViewModel model = new EditProfileViewModel
        {
            Bio = user.Bio,
            CurrentAvatarPath = user.AvatarPath
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProfileViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        string userId = userManager.GetUserId(User)!;
        bool success = await userService.UpdateUserProfileAsync(userId, model);

        if (success)
        {
            return RedirectToAction("Index");
        }

        return View(model);
    }
}
