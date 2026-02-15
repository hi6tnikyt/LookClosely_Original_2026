using System.ComponentModel.DataAnnotations;

namespace LookClosely_Original.LookCloselyViewModels
{
    public class ScoreViewModel
    {
        [Required(ErrorMessage = "Името на потребителя е задължително")]
        public string UserName { get; set; } = null!;
        public string LevelName { get; set; } = null!;
        public int Points { get; set; }
        public DateTime DateTime { get; set; }
    }
}
