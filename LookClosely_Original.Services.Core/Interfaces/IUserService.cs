using LookClosely_Original.LookCloselyViewModels.Event;
using LookClosely_Original.Data.Models;

namespace LookClosely_Original.Services.Core.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetUserProfileAsync(string userId);
        Task<bool> UpdateUserProfileAsync(string userId, EditProfileViewModel model);
    }
}