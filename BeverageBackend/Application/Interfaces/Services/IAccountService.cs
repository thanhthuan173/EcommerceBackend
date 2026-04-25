using BeverageBackend.Application.Dto.Auth;
using BeverageBackend.Application.Dto;
using BeverageBackend.Domain.Models;
using System.Security.Claims;

namespace BeverageBackend.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task Register(RegisterDto dto);
        Task<TokenDto> Login(LoginDto dto);
        Task<TokenDto> RefreshToken(RefreshTokenDto dto);
        Task Logout(RefreshTokenDto dto);
    }
}
