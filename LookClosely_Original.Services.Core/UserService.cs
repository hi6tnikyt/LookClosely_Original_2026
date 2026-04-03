using Microsoft.AspNetCore.Identity;
using LookClosely_Original.LookCloselyViewModels.Event;
using LookClosely_Original.Services.Core.Interfaces;
using LookClosely_Original.Data.Models;
using LookClosely_Original.Data.Repository.Contracts;

namespace LookClosely_Original.Services.Core
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserRepository userRepository;

        public UserService(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public async Task<ApplicationUser?> GetUserProfileAsync(string userId)
        {
            return await userRepository.GetByIdWithScoresAsync(userId);
                
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
                if (!string.IsNullOrEmpty(user.AvatarPath) && !user.AvatarPath.Contains("default.png"))
                {
                    string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.AvatarPath.TrimStart('/'));
                    if (File.Exists(oldFilePath)) { File.Delete(oldFilePath); }
                }

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
