using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Dto.Auth;
using BeverageBackend.Interfaces;
using BeverageBackend.Interfaces.Services;
using BeverageBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BeverageBackend.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _user;
        private readonly ICartRepository _cart;
        private readonly IRoleRepository _role;
        private readonly IUserRoleService _userRole;
        private readonly IMapper _mapper;
        private readonly BeverageDbContext _context;

        public AccountService(IUserRepository user,ICartRepository cart,IRoleRepository role,IUserRoleService userRole,IMapper mapper, BeverageDbContext context)
        {
            _user = user;
            _cart = cart;
            _role = role;
            _userRole = userRole;
            _mapper = mapper;
            _context = context;
        }

        public async Task Register(RegisterDto dto)
        {
            var isEmailExisted = await _user.GetByEmail(dto.Email);
            if (isEmailExisted != null)
            {
                throw new Exception("Email already existed");
            }
            var isUsernameExisted = await _user.GetByUsername(dto.Username);
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
            await _userRole.AddAsync(user.Id, customerRole.Id);
            await _context.SaveChangesAsync();
        }
    }
}
