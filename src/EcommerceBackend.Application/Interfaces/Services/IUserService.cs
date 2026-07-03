using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Dto.User;

namespace EcommerceBackend.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ProfileDto> GetProfileAsync();
        Task UpdateProfileAsync(UpdateProfileDto dto);
        Task<UserDetailDto> GetUserByIdAsync(int id);
        Task<PagedResult<UserDto>> GetUsersAsync(UserQueryParameters query);
        Task DeactivateUserAsync(int id);
        Task ActivateUserAsync(int id);
    }
}