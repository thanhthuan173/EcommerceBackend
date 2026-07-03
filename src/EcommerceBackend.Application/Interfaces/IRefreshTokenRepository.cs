using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<RefreshToken?> GetByTokenWithUserAsync(string token);
        Task<IEnumerable<RefreshToken>> GetByUserAsync(int userId);
        void Add(RefreshToken refreshToken);
    }
}
