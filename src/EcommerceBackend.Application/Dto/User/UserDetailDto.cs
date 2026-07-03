namespace EcommerceBackend.Application.Dto.User
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}