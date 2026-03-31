using LookClosely_Original.Data.Seeding.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseDbSeeder(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                IDbSeeder seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();

                seeder.SeedAsync(scope.ServiceProvider).GetAwaiter().GetResult();
            }

            return app;
        }
    }
}
