using EcommerceBackend.Application.Dto.Auth;
using EcommerceBackend.Application.Dto;
using EcommerceBackend.Domain.Models;
using System.Security.Claims;

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
