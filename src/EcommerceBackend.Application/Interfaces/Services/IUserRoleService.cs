using EcommerceBackend.Application.Dto.Role;
using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Interfaces.Services
{
    public interface IUserRoleService
    {
        Task AddAsync(UserRole userRole);
        Task<List<UserRoleDto>> GetUserRolesAsync();
        Task<List<UserRoleDto>> GetByUserAsync(int userId);
        Task RemoveRoleFromUser(int userId, int roleId);
    }
}
