using System.ComponentModel.DataAnnotations;
using static System.Formats.Asn1.AsnWriter;
using static LookClosely_Original.Common.EntityValidationConstants.Level;

namespace LookClosely.Models
{
    public class Level
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на нивото е задължително")]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public string ImagePath { get; set; } = null!;

        [Range(1, 10)]
        public int Difficulty { get; set; }

        public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
