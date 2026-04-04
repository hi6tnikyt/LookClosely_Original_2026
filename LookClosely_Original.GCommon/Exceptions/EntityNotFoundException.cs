
namespace LookClosely_Original.GCommon.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
            : base(ErrorMessages.LevelNotFound) { } 

        public EntityNotFoundException(string message) 
            : base(message) { }
    }
}
