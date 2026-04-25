using BeverageBackend.Application.Dto.Role;
using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Interfaces.Services
{
    public interface IUserRoleService
    {
        Task AddAsync(UserRole userRole);
        Task<List<UserRoleDto>> GetUserRolesAsync();
        Task<List<UserRoleDto>> GetByUserAsync(int userId);
        Task RemoveRoleFromUser(int userId, int roleId);
    }
}
