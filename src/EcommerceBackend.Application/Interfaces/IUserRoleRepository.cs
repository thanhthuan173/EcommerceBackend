using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Interfaces
{
    public interface IUserRoleRepository
    {
        void Add(UserRole userRole);
        Task<List<UserRole>> GetUserRolesAsync();
        Task<UserRole?> GetUserRoleAsync(int userId, int roleId);
        Task<List<UserRole>> GetByUserAsync(int userId);
        Task<List<string>> GetRoleNameByUserAsync(int userId);
        Task<bool> IsExisted(int userId,int roleId);
        void Delete(UserRole userRole);
    }
}
