namespace LookClosely_Original.Common
{
    public static class EntityValidationConstants
    {
        public static class Level
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 150;
        }

        public static class Score
        {
            public const int MinPoints = 0;
            public const int MaxPoints = 100000;
        }

        public static class ApplicationUser
        {
            public const int BioMaxLength = 500;
            public const int UserNameMinLength = 5;
            public const int UserNameMaxLength = 150;
        }

    }
}
