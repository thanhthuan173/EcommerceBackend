using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Domain.Models;
using EcommerceBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly EcommerceDbContext _context;

        public RefreshTokenRepository(EcommerceDbContext context)
        {
            _context = context;
        }
        public void Add(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<RefreshToken?> GetByTokenWithUserAsync(string token)
        {
            return await _context.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserAsync(int userId)
        {
            return await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
        }
    }
}
