using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Application.Dto.Product
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
