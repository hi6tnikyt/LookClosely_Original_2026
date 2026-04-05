
using LookClosely.Infrastructure.Utilities.Contracts;

namespace LookClosely.Infrastructure.Utilities
{
    public class SlugGenerator : ISlugGenerator
    {
        public string GenerateSlug(string input)
        {
            string[] inputDataSplit = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLowerInvariant())
                .ToArray();

            return string.Join('-', inputDataSplit);
                
        }
    }
}
