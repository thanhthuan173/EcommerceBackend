using BeverageBackend.Application.Interfaces.Services;
using System.Security.Claims;

namespace BeverageBackend.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                    throw new UnauthorizedAccessException("No HttpContext");
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    throw new UnauthorizedAccessException("UserId claim not found");
                if (!int.TryParse(userIdClaim.Value, out int userId))
                    throw new UnauthorizedAccessException("Invalid user id");
                return userId;
            }
        }
    }
}
