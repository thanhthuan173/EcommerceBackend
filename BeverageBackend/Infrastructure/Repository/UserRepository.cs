using BeverageBackend.Application.Common;
using BeverageBackend.Application.Common.Query;
using BeverageBackend.Application.Interfaces;
using BeverageBackend.Domain.Models;
using BeverageBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeverageBackend.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BeverageDbContext _context;

        public UserRepository(BeverageDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByIdWithRolesAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameOrEmailAsync(string account)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == account || u.Email == account);
        }

        public async Task<PagedResult<User>> GetAllAsync(UserQueryParameters query)
        {
            var users = _context.Users.Include(u=>u.UserRoles).ThenInclude(ur=>ur.Role).AsQueryable();

            if (!string.IsNullOrEmpty(query.FullName))
            {
                users = users.Where(u => u.FullName.ToLower().Contains(query.FullName.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.Gender))
            {
                users = users.Where(u => u.Gender.ToLower()==query.Gender.Trim().ToLower());
            }
            if (!string.IsNullOrEmpty(query.Address))
            {
                users = users.Where(u => u.Address.ToLower().Contains(query.Address.ToLower()));
            }
            if (query.FromDate.HasValue)
            {
                users = users.Where(u => u.CreatedAt >= query.FromDate);
            }
            if (query.ToDate.HasValue)
            {
                users = users.Where(u => u.CreatedAt < query.ToDate.Value.Date.AddDays(1));
            }
            if (query.IsActive.HasValue)
            {
                users = users.Where(u => u.IsActive == query.IsActive);
            }
            if (!string.IsNullOrEmpty(query.Role))
            {
                users = users.Where(u=>u.UserRoles.Any(ur=>ur.Role.Name.ToLower()==query.Role.ToLower()));
            }

            switch (query.SortBy?.ToLower())
            {
                case "createdat":
                    users = query.Desc ? users.OrderByDescending(u => u.CreatedAt) : users.OrderBy(u => u.CreatedAt);
                    break;
                case "fullname":
                    users = query.Desc ? users.OrderByDescending(u => u.FullName) : users.OrderBy(u => u.FullName);
                    break;
                default:
                    users = users.OrderBy(u => u.Id);
                    break;
            }

            var totalCount = await users.CountAsync();
            var items = await users
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PagedResult<User>(items, totalCount, query.PageNumber, query.PageSize);
        }

        public void Add(User user)
        {
            _context.Add(user);
        }

        public void Update(User user)
        {
            _context.Update(user);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}
