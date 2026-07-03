using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Domain.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Role
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
