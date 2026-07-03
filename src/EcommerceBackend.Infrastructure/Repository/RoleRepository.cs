using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Domain.Models;
using EcommerceBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EcommerceDbContext _context;

        public RoleRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public void Add(Role role)
        {
            _context.Roles.AddAsync(role);
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public Task<bool> RoleExisted(string roleName, int excludeId)
        {
            return _context.Roles.AnyAsync(r => r.Name == roleName && r.Id != excludeId);
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
        }

        public async Task<bool> IsRoleUsed(int id)
        {
            return await _context.UserRoles.AnyAsync(ur => ur.RoleId == id);
        }
    }
}
