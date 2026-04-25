using System.ComponentModel.DataAnnotations.Schema;

namespace BeverageBackend.Application.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
