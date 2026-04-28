namespace BeverageBackend.Application.Exceptions
{
    public class AlreadyExistsException : AppException
    {
        public AlreadyExistsException(string message) : base(message, 409, "AlreadyExists")
        {
        }
    }
}
