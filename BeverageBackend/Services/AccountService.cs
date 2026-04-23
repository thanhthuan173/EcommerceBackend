using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Dto.Auth;
using BeverageBackend.Dto.Role;
using BeverageBackend.Interfaces;
using BeverageBackend.Interfaces.Services;
using BeverageBackend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeverageBackend.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _user;
        private readonly ICartRepository _cart;
        private readonly IRoleRepository _role;
        private readonly IUserRoleRepository _userRole;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly BeverageDbContext _context;

        public AccountService(IUserRepository user,ICartRepository cart,IRoleRepository role, IUserRoleRepository userRole,ITokenService tokenService,IMapper mapper, BeverageDbContext context)
        {
            _user = user;
            _cart = cart;
            _role = role;
            _userRole = userRole;
            _tokenService = tokenService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<TokenDto> Login(LoginDto dto)
        {
            var user = await _user.GetByUsernameOrEmailAsync(dto.Account);
            if (user == null)
                throw new Exception("User not found");
            var isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.HashPassword);
            if (!isValidPassword)
                throw new Exception("Invalid password");
            if (!user.IsActive)
                throw new Exception("User is inactive");
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var roles = await _userRole.GetRoleNameByUserAsync(user.Id);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var refreshEntity = new RefreshToken()
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };
            _context.RefreshTokens.Add(refreshEntity);
            await _context.SaveChangesAsync();
            return new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task Logout(RefreshTokenDto dto)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);
            if (token != null)
            {
                token.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TokenDto> RefreshToken(RefreshTokenDto dto)
        {
            var oldRefreshTokenEntity = await _context.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);
            if (oldRefreshTokenEntity == null)
                throw new Exception("Invalid refresh token");
            if (oldRefreshTokenEntity.IsUsed)
            {
                await _tokenService.RevokeAllUserTokens(oldRefreshTokenEntity.UserId);
                throw new Exception("Reuse detected");
            }
            if (oldRefreshTokenEntity.ExpiresAt<DateTime.UtcNow)
                throw new Exception("Refresh token expired");
            //logout,change password, admin revoke,revoke all user
            if (oldRefreshTokenEntity.IsRevoked)
                throw new Exception("Invalid refresh token");
            oldRefreshTokenEntity.IsRevoked = true;
            oldRefreshTokenEntity.IsUsed = true;
            var user = oldRefreshTokenEntity.User;
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var roles = await _userRole.GetRoleNameByUserAsync(user.Id);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var newRefreshTokenEntity = new RefreshToken()
            {
                Token = newRefreshToken,
                UserId = oldRefreshTokenEntity.UserId,
                ExpiresAt = _tokenService.GetRefreshTokenExpiry(),
                IsRevoked = false,
                IsUsed=false
            };
            _context.RefreshTokens.Add(newRefreshTokenEntity);
            await _context.SaveChangesAsync();
            return new TokenDto()
            {
                AccessToken= accessToken,
                RefreshToken= newRefreshToken
            };
        }

        public async Task Register(RegisterDto dto)
        {
            var isEmailExisted = await _user.GetByEmailAsync(dto.Email);
            if (isEmailExisted != null)
            {
                throw new Exception("Email already existed");
            }
            var isUsernameExisted = await _user.GetByUsernameAsync(dto.Username);
            if (isUsernameExisted != null)
            {
                throw new Exception("Username already existed");
            }
            var user = _mapper.Map<User>(dto);
            user.HashPassword= BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _user.Add(user);
            var cart = new Cart()
            {
                User = user
            };
            _cart.CreateCart(cart);
            var customerRole = await _role.GetByNameAsync("CUSTOMER");
            if (customerRole == null)
            {
                throw new Exception("CUSTOMER role not found");
            }
            var userRole = new UserRole()
            {
                User = user,
                RoleId = customerRole.Id
            };
            _userRole.Add(userRole);
            await _context.SaveChangesAsync();
        }
    }
}
