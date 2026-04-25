using System.Security.Claims;

namespace BeverageBackend.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(List<Claim> claims);
        string GenerateRefreshToken();
        Task RevokeAllUserTokens(int userId);
        DateTime GetRefreshTokenExpiry();
    }
}
