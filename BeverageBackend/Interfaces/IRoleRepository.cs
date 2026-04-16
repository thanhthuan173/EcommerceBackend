using BeverageBackend.Models;

namespace BeverageBackend.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetRolesAsync();
        Task<bool> RoleExisted(string roleName, int excludeId);
        void Add(Role role);
        Task<Role?> GetByNameAsync(string roleName);
        Task<Role?> GetByIdAsync(int id);
        Task<bool> IsRoleUsed(int id);
        void Delete(Role role);
    }
}
