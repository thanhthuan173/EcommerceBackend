namespace BeverageBackend.Application.Exceptions.Token
{
    public class TokenReuseException : AppException
    {
        public TokenReuseException(string message) : base(message, 401, "TokenReuse")
        {
        }
    }
}
