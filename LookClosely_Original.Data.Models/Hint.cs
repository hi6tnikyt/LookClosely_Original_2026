using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LookClosely_Original.Data.Models
{
    public class Hint
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        public int PointsRequired { get; set; } 

        public int LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public virtual Level Level { get; set; } = null!;
    }
}
