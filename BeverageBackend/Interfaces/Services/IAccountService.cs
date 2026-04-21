using BeverageBackend.Dto;
using BeverageBackend.Dto.Auth;
using BeverageBackend.Models;
using System.Security.Claims;

namespace BeverageBackend.Interfaces.Services
{
    public interface IAccountService
    {
        Task Register(RegisterDto dto);
        Task<TokenDto> Login(LoginDto dto);
        Task<TokenDto> RefreshToken(RefreshTokenDto dto);
        Task Logout(RefreshTokenDto dto);
    }
}
