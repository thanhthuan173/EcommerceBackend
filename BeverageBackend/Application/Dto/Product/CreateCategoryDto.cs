using System.ComponentModel.DataAnnotations;

namespace BeverageBackend.Application.Dto.Product
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
