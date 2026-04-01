using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LookClosely_Original.GCommon.EntityValidationConstants.Score;

namespace LookClosely_Original.Data.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(MinPoints, MaxPoints)]
        public int Points { get; set; }

        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public int LevelId { get; set; }

        [ForeignKey(nameof(LevelId))]
        public virtual Level Level { get; set; } = null!;
    }
}