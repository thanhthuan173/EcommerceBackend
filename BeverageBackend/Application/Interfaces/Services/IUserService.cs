using BeverageBackend.Application.Common;
using BeverageBackend.Application.Common.Query;
using BeverageBackend.Application.Dto.User;

namespace BeverageBackend.Application.Interfaces.Services
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