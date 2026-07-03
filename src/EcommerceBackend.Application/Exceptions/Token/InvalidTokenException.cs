namespace EcommerceBackend.Application.Exceptions.Token
{
    public class InvalidTokenException : AppException
    {
        public InvalidTokenException(string message) : base(message, 401, "InvalidToken")
        {
        }
    }
}
