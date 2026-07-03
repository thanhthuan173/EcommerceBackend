using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public string? Gender { get; set; }
        public required string Phone { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public string? Address { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string HashPassword { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public Cart Cart { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
