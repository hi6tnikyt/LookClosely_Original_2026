using System.ComponentModel.DataAnnotations;
using static LookClosely_Original.GCommon.EntityValidationConstants.Level;
using static LookClosely_Original.GCommon.Exceptions.ErrorMessages;

namespace LookClosely_Original.Data.Models
{
    public class Level
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        [Required(ErrorMessage = LevelNameRequired)]
        public string Name { get; set; } = null!;

        public double TargetX { get; set; } 
        public double TargetY { get; set; } 
        public double TargetRadius { get; set; } = 5.0;

        [Required]
        [MaxLength(TargetObjectNameMaxLength)]
        public string TargetObjectName { get; set; } = null!;

        [Required]
        public string Difficulty { get; set; } = null!;

        [Required(ErrorMessage = LevelImageRequired)]
        public string? ImagePath { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Score> Scores { get; set; }
              = new List<Score>();
    }
}
