
namespace LookClosely_Original.GCommon.Exceptions
{
    public class EntityInputDataException : Exception
    {
        public EntityInputDataException()
             : base(ErrorMessages.UnexpectedError) { }

        public EntityInputDataException(string message)
            : base(message) { }
    }
}
