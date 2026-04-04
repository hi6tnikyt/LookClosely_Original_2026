using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LookClosely_Original.LookCloselyViewModels.Event;
using LookClosely_Original.Services.Core.Interfaces;
using LookClosely_Original.Data.Models;
using LookClosely_Original.GCommon.Exceptions;

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
        try
        {
            string userId = userManager.GetUserId(User)!;
            ApplicationUser user = await userService.GetUserProfileAsync(userId);

            return View(user);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            string userId = userManager.GetUserId(User)!;

            await userService.UpdateUserProfileAsync(userId, model);

            return RedirectToAction("Index");
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (EntityEditPersistFailException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }
}
