using System.ComponentModel.DataAnnotations;
using static LookClosely_Original.GCommon.EntityValidationConstants.Level;
using static LookClosely_Original.GCommon.Exceptions.ErrorMessages;

namespace LookClosely_Original.ViewModels
{
    public class LevelViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = LevelNameRequired)]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = LevelImageRequired)]
        public string ImagePath { get; set; } = null!;

        public string? Difficulty { get; set; }

        [Range(0, 2000, ErrorMessage = InvalidCoordinates)] 
        public double TargetX { get; set; }

        [Range(0, 2000, ErrorMessage = InvalidCoordinates)]
        public double TargetY { get; set; }

        [Range(1, 500, ErrorMessage = InvalidRadius)] 
        public double TargetRadius { get; set; }

        [Required]
        [MaxLength(TargetObjectNameMaxLength)]
        public string TargetObjectName { get; set; } = null!;
    }
}
