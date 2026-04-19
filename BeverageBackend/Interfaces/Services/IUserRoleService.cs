using BeverageBackend.Dto.Role;
using BeverageBackend.Models;

namespace BeverageBackend.Interfaces.Services
{
    public interface IUserRoleService
    {
        Task AddAsync(UserRole userRole);
        Task<List<UserRoleDto>> GetUserRolesAsync();
        Task<List<UserRoleDto>> GetByUserAsync(int userId);
        Task RemoveRoleFromUser(int userId, int roleId);
    }
}
