namespace EcommerceBackend.Application.Dto.Auth
{
    public class TokenDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
