using System.ComponentModel.DataAnnotations;

namespace BeverageBackend.Dto
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
