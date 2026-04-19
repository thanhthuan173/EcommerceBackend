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

        public async Task<string> Login(LoginDto dto)
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
            var token = _tokenService.GenerateToken(claims);
            return token;
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
