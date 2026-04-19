using System.Security.Claims;

namespace BeverageBackend.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(List<Claim> claims);
    }
}
