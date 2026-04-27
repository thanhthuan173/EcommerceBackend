using System.ComponentModel.DataAnnotations;

namespace BeverageBackend.Application.Dto
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
