using EcommerceBackend.Application.Dto.Auth;

namespace EcommerceBackend.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task Register(RegisterDto dto);
        Task<TokenDto> Login(LoginDto dto);
        Task<TokenDto> RefreshToken(RefreshTokenDto dto);
        Task Logout(RefreshTokenDto dto);
    }
}
