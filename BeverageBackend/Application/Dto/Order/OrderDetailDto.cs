using System.ComponentModel.DataAnnotations.Schema;

namespace BeverageBackend.Application.Dto.Order
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
