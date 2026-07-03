namespace EcommerceBackend.Application.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string msg) : base(msg, 403, "Forbidden")
        {
        }
    }
}
