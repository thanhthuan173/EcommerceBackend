using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeverageBackend.Application.Dto.Cart
{
    public class AddCartItemDto
    {
        public int ProductId { get; set; }
        [DefaultValue(1)]
        [Range(1, 50, ErrorMessage = "The quantity must be between 1 and 50.")]
        public int Quantity { get; set; } = 1;
    }
}
