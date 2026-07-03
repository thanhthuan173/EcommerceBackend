namespace EcommerceBackend.Application.Dto.Order
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
