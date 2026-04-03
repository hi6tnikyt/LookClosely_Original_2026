
namespace LookClosely_Original.GCommon.Exceptions
{
    public static class ErrorMessages
    {
        // Levels
        public const string LevelNameRequired = "Името на нивото е задължително!";
        public const string LevelImageRequired = "Снимката е задължителна!";
        public const string InvalidCoordinates = "Координатите трябва да са между 0 и 100 процента.";
        public const string InvalidRadius = "Радиусът трябва да е между 1 и 20.";
        public const string LevelNotFound = "Нивото не беше намерено!";
        public const string LevelNameAlreadyExists = "Ниво с това име вече съществува.";

        // General
        public const string UnexpectedError = "Възникна неочаквана грешка. Моля, опитайте пак.";
        public const string AccessDenied = "Нямате достъп до тази функционалност!";
    }
}
