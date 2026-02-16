using System.ComponentModel.DataAnnotations;
using static LookClosely_Original.GCommon.EntityValidationConstants.Level;
namespace LookClosely_Original.ViewModels
{
    public class LevelViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на нивото е задължително.")]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Снимката е задължителна.")]
        public string ImagePath { get; set; } = null!;

        public string? Difficulty { get; set; }
    }
}
