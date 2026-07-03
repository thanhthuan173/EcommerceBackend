namespace EcommerceBackend.Application.Dto.Auth
{
    public class RegisterDto
    {
        public required string FullName { get; set; }
        public string? Gender { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public string? Address { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
