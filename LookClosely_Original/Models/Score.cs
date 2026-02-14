using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static LookClosely_Original.Common.EntityValidationConstants.Score;

namespace LookClosely.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxPoints)]
        public int Points { get; set; }

        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [Required]
        public string UserId { get; set; } = null!;
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; } = null!;

        [Required]
        public int LevelId { get; set; }
        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; } = null!;
    }
}