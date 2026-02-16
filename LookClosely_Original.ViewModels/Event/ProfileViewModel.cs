using System.ComponentModel.DataAnnotations;
using LookClosely_Original.ViewModels;
using static LookClosely_Original.GCommon.EntityValidationConstants.Level;
using static LookClosely_Original.GCommon.EntityValidationConstants.ApplicationUser;

namespace LookClosely_Original.LookCloselyViewModels.Event
{
    public class ProfileViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Username { get; set; } = null!;

        [StringLength(BioMaxLength, ErrorMessage = "Биографията е твърде дълга.")]
        public string? Bio { get; set; }
        public string? AvatarPath { get; set; }

        public List<ScoreViewModel> TopScores { get; set; } = new();

        public double CompletionPercentage { get; set; }
    }
}
