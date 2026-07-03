using EcommerceBackend.Domain.Enums;

namespace EcommerceBackend.Application.Dto.Order
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public required OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
