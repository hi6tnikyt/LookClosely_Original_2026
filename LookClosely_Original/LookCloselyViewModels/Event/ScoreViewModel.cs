namespace LookClosely_Original.LookCloselyViewModels.Event
{
    public class ScoreViewModel
    {
        public int Rank { get; set; }
        public string UserName { get; set; } = null!;
        public string LevelName { get; set; } = null!;
        public int Points { get; set; }
        public string DateAchieved { get; set; } = null!;
    }
}
