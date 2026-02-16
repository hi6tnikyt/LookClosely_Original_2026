using System.ComponentModel.DataAnnotations;
using static LookClosely_Original.GCommon.EntityValidationConstants.ApplicationUser;
using Microsoft.AspNetCore.Http;

namespace LookClosely_Original.LookCloselyViewModels.Event
{
    public class EditProfileViewModel
    {
        [MaxLength(BioMaxLength)] 
        public string? Bio { get; set; }

        [Display(Name = "Профилна снимка")]
        public IFormFile? AvatarFile { get; set; } 

        public string? CurrentAvatarPath { get; set; }
    }
}
