using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Application.Dto.Product
{
    public class CreateCategoryDto
    {
        [Required]
        public required string Name { get; set; }
    }
}
