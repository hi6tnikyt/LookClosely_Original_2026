using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static LookClosely_Original.Common.EntityValidationConstants.ApplicationUser;

namespace LookClosely.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(UserNameMaxLength)]
        public string? AvatarPath { get; set; }

        public string? ProfilePicturePath { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(BioMaxLength)]
        public string? Bio { get; set; }

        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
