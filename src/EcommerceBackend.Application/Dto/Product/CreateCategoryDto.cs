using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Application.Dto.Product
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
