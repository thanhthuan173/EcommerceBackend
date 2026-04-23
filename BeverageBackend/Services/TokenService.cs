using BeverageBackend.Configuration;
using BeverageBackend.Interfaces.Services;
using BeverageBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BeverageBackend.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwt;
        private readonly BeverageDbContext _context;

        public TokenService(IOptions<JwtOptions> options,BeverageDbContext context)
        {
            _jwt = options.Value;
            _context = context;
        }
        public string GenerateAccessToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task RevokeAllUserTokens(int userId)
        {
            var userTokens = await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
            foreach(var token in userTokens)
            {
                token.IsRevoked = true;
            }
        }
    }
}
