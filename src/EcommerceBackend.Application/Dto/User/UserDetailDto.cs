using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Dto.User
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
    }
}