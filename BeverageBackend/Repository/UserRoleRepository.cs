using BeverageBackend.Interfaces;
using BeverageBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BeverageBackend.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly BeverageDbContext _context;

        public UserRoleRepository(BeverageDbContext context)
        {
            _context = context;
        }
        public void Add(UserRole userRole)
        {
            _context.Add(userRole);
        }

        public void Delete(UserRole userRole)
        {
            _context.Remove(userRole);
        }

        public async Task<List<string>> GetRoleNameByUserAsync(int userId)
        {
            return await _context.UserRoles.Where(ur => ur.UserId == userId).Select(ur => ur.Role.Name).ToListAsync();
        }

        public async Task<List<UserRole>> GetByUserAsync(int userId)
        {
            return await _context.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();
        }

        public async Task<UserRole?> GetUserRoleAsync(int userId, int roleId)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public async Task<List<UserRole>> GetUserRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<bool> IsExisted(int userId, int roleId)
        {
            return await _context.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
    }
}
