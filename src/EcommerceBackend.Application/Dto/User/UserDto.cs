namespace EcommerceBackend.Application.Dto.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
