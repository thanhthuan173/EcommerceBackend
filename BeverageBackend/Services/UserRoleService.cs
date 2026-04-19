using BeverageBackend.Dto.Role;
using BeverageBackend.Interfaces;
using BeverageBackend.Interfaces.Services;
using BeverageBackend.Models;
using System.Data;

namespace BeverageBackend.Services
{
    public class UserRoleService:IUserRoleService
    {
        private readonly IUserRoleRepository _repo;
        private readonly BeverageDbContext _context;

        public UserRoleService(IUserRoleRepository repo,BeverageDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task AddAsync(UserRole userRole)
        {
            
            _repo.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserRoleDto>> GetUserRolesAsync()
        {
            var userRoles = await _repo.GetUserRolesAsync();
            return userRoles.Select(ur => new UserRoleDto()
            {
                UserName = ur.User.Username,
                RoleName = ur.Role.Name
            }).ToList();
        }

        public async Task<List<UserRoleDto>> GetByUserAsync(int userId)
        {
            var roles = await _repo.GetByUserAsync(userId);
            return roles.Select(ur => new UserRoleDto()
            {
                UserName = ur.User.Username,
                RoleName = ur.Role.Name
            }).ToList();
        }

        public async Task RemoveRoleFromUser(int userId, int roleId)
        {
            var userRole = await _repo.GetUserRoleAsync(userId,roleId);
            if (userRole == null)
                return;
            _repo.Delete(userRole);
            await _context.SaveChangesAsync();
        }
    }
}
