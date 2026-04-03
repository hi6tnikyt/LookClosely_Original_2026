
using LookClosely_Original.Data.Models;

namespace LookClosely_Original.Data.Repository.Contracts
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<int> SaveChangeAsync();
    }
}
