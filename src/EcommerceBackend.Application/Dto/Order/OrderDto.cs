namespace EcommerceBackend.Application.Dto.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
