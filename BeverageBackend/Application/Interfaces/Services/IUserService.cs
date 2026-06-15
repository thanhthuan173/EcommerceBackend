using BeverageBackend.Application.Dto.User;

namespace BeverageBackend.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ProfileDto> GetProfileAsync();
        Task UpdateProfileAsync(UpdateProfileDto dto);
        Task<UserDetailDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task DeactivateUserAsync(int id);
        Task ActivateUserAsync(int id);
    }
}