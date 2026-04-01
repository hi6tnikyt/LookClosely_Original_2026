using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LookClosely_Original.Data.Models
{
    public class UserHint
    {
        [Required]
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public int HintId { get; set; }
        [ForeignKey(nameof(HintId))]
        public virtual Hint Hint { get; set; } = null!;

        public DateTime UnlockedOn { get; set; }
    }
}
