using System.ComponentModel.DataAnnotations;
using static LookClosely_Original.Common.EntityValidationConstants.Level;

namespace LookClosely.Models
{
    public class Level
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        [Required(ErrorMessage = "Името е задължително!")]
        public string Name { get; set; } = null!;

        [Required]
        public string Difficulty { get; set; } = null!;

        [Required(ErrorMessage = "Снимката е задължителна!")]
        public string? ImagePath { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
              = new List<Score>();
    }
}
