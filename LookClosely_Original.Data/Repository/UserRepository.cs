
using LookClosely_Original.Data.Models;
using LookClosely_Original.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LookClosely_Original.Data.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await DbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await DbContext.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public new async Task<int> SaveChangeAsync()
        {
            return await base.SaveChangeAsync();
        }
    }
}
