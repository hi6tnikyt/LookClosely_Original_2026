using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static LookClosely_Original.GCommon.EntityValidationConstants.Score;

namespace LookClosely.Models
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
        [ForeignKey("UserId")]
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey("LevelId")]
        public int LevelId { get; set; }
        public virtual Level Level { get; set; } = null!;
    }
}