
namespace LookClosely_Original.GCommon.Exceptions
{
    public class EntityEditPersistFailException : Exception
    {
        public EntityEditPersistFailException()
            : base("Възникна грешка при опит за обновяване на данните в базата.")
        {
        }

        public EntityEditPersistFailException(string message)
            : base(message)
        {
        }
    }
}
