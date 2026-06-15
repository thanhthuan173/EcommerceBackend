using System.ComponentModel.DataAnnotations;

namespace BeverageBackend.Application.Dto.Product
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
