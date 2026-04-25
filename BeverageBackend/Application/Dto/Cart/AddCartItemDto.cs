using System.ComponentModel.DataAnnotations;

namespace BeverageBackend.Application.Dto.Cart
{
    public class AddCartItemDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        [Range(1, 100, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
    }
}
