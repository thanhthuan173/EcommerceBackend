namespace EcommerceBackend.Application.Dto.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
        public decimal TotalAmount { get; set; }
    }
}
