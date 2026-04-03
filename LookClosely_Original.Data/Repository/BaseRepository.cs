using LookClosely_Original.Data;
using Microsoft.EntityFrameworkCore;

namespace LookClosely_Original.Data.Repository
{
    public abstract class BaseRepository : IDisposable
    {
        private bool isDisposed = false;
        private readonly ApplicationDbContext dbContext;

        protected BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected async Task<int> SaveChangeAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        protected ApplicationDbContext DbContext => dbContext;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            isDisposed = true;
        }
    }
}
