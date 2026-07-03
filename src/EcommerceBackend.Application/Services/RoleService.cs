using EcommerceBackend.Application.Dto.Role;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Application.Interfaces.Services;
using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RolesDto>> GetRolesAsync()
        {
            var roles = await _repo.GetRolesAsync();
            return roles.Select(r => new RolesDto()
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
        }

        public async Task CreateAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Name is required");
            roleName = roleName.Trim().ToUpper();
            if (await _repo.GetByNameAsync(roleName) != null)
                throw new Exception("Role already exists");
            var newRole = new Role()
            {
                Name = roleName
            };
            _repo.Add(newRole);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role = await _repo.GetByIdAsync(id);
            if (role == null)
                return null;
            return new RoleDto()
            {
                Name = role.Name
            };
        }

        public async Task<RoleDto?> GetByNameAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Name is required");
            roleName = roleName.Trim().ToUpper();
            var role = await _repo.GetByNameAsync(roleName);
            if (role == null)
                return null;
            return new RoleDto()
            {
                Name = role.Name
            };
        }

        public async Task UpdateNameAsync(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");
            name = name.Trim().ToUpper();
            var role = await _repo.GetByIdAsync(id);
            if (role == null)
                throw new Exception("Role not found");
            if (await _repo.RoleExisted(name, id))
                throw new Exception("Role already existed");
            role.Name = name;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _repo.GetByIdAsync(id);
            if (role == null)
                throw new KeyNotFoundException("Role not found");
            if (await _repo.IsRoleUsed(id))
                throw new Exception("Role is in use");
            _repo.Delete(role);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
