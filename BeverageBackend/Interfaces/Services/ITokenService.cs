using System.Security.Claims;

namespace BeverageBackend.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(List<Claim> claims);
        string GenerateRefreshToken();
    }
}
