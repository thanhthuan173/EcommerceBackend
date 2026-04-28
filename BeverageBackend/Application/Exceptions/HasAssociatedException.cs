namespace BeverageBackend.Application.Exceptions
{
    public class HasAssociatedException : AppException
    {
        public HasAssociatedException(string message) : base(message, 409, "HasAssociated")
        {
        }
    }
}
