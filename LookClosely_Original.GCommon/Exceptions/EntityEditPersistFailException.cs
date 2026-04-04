
namespace LookClosely_Original.GCommon.Exceptions
{
    public class EntityEditPersistFailException : Exception
    {
        public EntityEditPersistFailException()
            : base(ErrorMessages.UpdateFailed)
        {
        }

        public EntityEditPersistFailException(string message)
            : base(message)
        {
        }
    }
}
