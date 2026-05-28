using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<RefreshToken?> GetByTokenWithUserAsync(string token);
        Task<IEnumerable<RefreshToken>> GetByUserAsync(int userId);
        void Add(RefreshToken refreshToken);
    }
}
