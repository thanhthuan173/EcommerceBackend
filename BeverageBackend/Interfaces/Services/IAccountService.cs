using BeverageBackend.Dto;
using BeverageBackend.Dto.Auth;
using BeverageBackend.Models;

namespace BeverageBackend.Interfaces.Services
{
    public interface IAccountService
    {
        Task Register(RegisterDto user);
    }
}
