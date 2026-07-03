using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Application.Dto.Product
{
    public class UpdateCategoryDto
    {
        [Required]
        public required string Name { get; set; }
    }
}
