
using LookClosely_Original.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LookClosely_Original.Data.Configurations
{
    internal class UserHintConfiguration : IEntityTypeConfiguration<UserHint>
    {
        public void Configure(EntityTypeBuilder<UserHint> entity)
        {
            entity.HasKey(uh => new { uh.UserId, uh.HintId });
        }
    }
}
