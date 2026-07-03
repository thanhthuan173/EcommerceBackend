using EcommerceBackend.Application.Dto.Role;
using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<List<RolesDto>> GetRolesAsync();
        Task CreateAsync(string roleName);
        Task<RoleDto?> GetByNameAsync(string roleName);
        Task<RoleDto?> GetByIdAsync(int id);
        Task UpdateNameAsync(int id, string name);
        Task DeleteAsync(int id);
    }
}
