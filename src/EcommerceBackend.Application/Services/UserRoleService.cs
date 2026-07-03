using EcommerceBackend.Application.Dto.Role;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Application.Interfaces.Services;
using EcommerceBackend.Domain.Models;
using EcommerceBackend.Infrastructure.Persistence;
using System.Data;

namespace EcommerceBackend.Application.Services
{
    public class UserRoleService:IUserRoleService
    {
        private readonly IUserRoleRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleService(IUserRoleRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(UserRole userRole)
        {
            
            _repo.Add(userRole);
            await _unitOfWork.SaveChangesAsync();
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
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
