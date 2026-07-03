namespace EcommerceBackend.Application.Exceptions.Token
{
    public class ExpiredTokenException : AppException
    {
        public ExpiredTokenException(string message) : base(message, 401, "TokenExpired")
        {
        }
    }
}
