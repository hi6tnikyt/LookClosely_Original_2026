
using LookClosely_Original.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LookClosely_Original.Data.Configurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> entity)
        {
            entity.HasOne(s => s.User)
                 .WithMany(u => u.Scores)
                 .HasForeignKey(s => s.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(s => s.Level)
                .WithMany(l => l.Scores) 
                .HasForeignKey(s => s.LevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
