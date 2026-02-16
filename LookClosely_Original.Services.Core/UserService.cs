using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LookClosely.Models;
using LookClosely_Original.LookCloselyViewModels.Event;
using LookClosely_Original.Services.Core.Interfaces;

namespace LookClosely_Original.Services.Core
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserProfileAsync(string userId)
        {
            return await userManager.Users
                .Include(u => u.Scores)
                .ThenInclude(s => s.Level)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> UpdateUserProfileAsync(string userId, EditProfileViewModel model)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            if (model.AvatarFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.AvatarFile.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/avatars", fileName);

                using (FileStream stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await model.AvatarFile.CopyToAsync(stream);
                }

                user.AvatarPath = "/images/avatars/" + fileName;
            }

            user.Bio = model.Bio;
            IdentityResult result = await userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}
