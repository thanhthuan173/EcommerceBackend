using AutoMapper;
using BeverageBackend.Application.Dto.User;
using BeverageBackend.Application.Exceptions;
using BeverageBackend.Domain.Models;

namespace BeverageBackend.Application.Interfaces.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public UserService(IUserRepository repo, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        private async Task<User> GetUserOrThrow(int id)
        {
            var user = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("User not found");
            return user;
        }

        public async Task<ProfileDto> GetProfileAsync()
        {
            var user = await GetUserOrThrow(_currentUser.UserId);
            return _mapper.Map<ProfileDto>(user);
        }

        public async Task UpdateProfileAsync(UpdateProfileDto dto)
        {
            var user = await GetUserOrThrow(_currentUser.UserId);
            if (dto.FullName is not null)
            {
                user.FullName = dto.FullName;
            }
            if (dto.Username is not null)
            {
                var existingUser =
                await _repo.GetByUsernameAsync(dto.Username);

                if (existingUser != null && existingUser.Id != user.Id)
                {
                    throw new AlreadyExistsException("Username already exists");
                }
                user.Username = dto.Username;
            }
            if (dto.Email is not null)
            {
                var existingUser =
                await _repo.GetByEmailAsync(dto.Email);

                if (existingUser != null && existingUser.Id != user.Id)
                {
                    throw new AlreadyExistsException("Email already exists");
                }
                user.Email = dto.Email;
            }
            if (dto.Gender is not null)
            {
                user.Gender = dto.Gender;
            }
            if (dto.Phone is not null)
            {
                user.Phone = dto.Phone;
            }
            if (dto.Address is not null)
            {
                user.Address = dto.Address;
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDetailDto> GetUserByIdAsync(int id)
        {
            var user = await _repo.GetByIdWithRolesAsync(id) ?? throw new NotFoundException("User not found");
            var mapper = _mapper.Map<UserDetailDto>(user);
            mapper.Roles=user.UserRoles.Select(ur=>ur.Role.Name).ToList();
            return mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return _mapper.Map<List<UserDto>>(await _repo.GetAllAsync());
        }

        public async Task ActivateUserAsync(int id)
        {
            var user = await GetUserOrThrow(id);
            if (user.IsActive)
            {
                throw new BadRequestException("User is already active");
            }
            user.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeactivateUserAsync(int id)
        {
            if(id == _currentUser.UserId)
            {
                throw new BadRequestException("Cannot deactivate yourself");
            }
            var user = await GetUserOrThrow(id);
            if (!user.IsActive)
            {
                throw new BadRequestException("User has been deactivated");
            }
            user.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}