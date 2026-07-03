namespace EcommerceBackend.Application.Dto.User
{
    public class ProfileDto
    {
        public required string FullName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string? Gender { get; set; }
        public required string Phone { get; set; }
        public string? Address { get; set; }
    }
}