using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookClosely_Original.Data.Seeding.Contracts
{
    public interface IDbSeeder
    {
        Task SeedAsync(IServiceProvider serviceProvider);
    }
}
