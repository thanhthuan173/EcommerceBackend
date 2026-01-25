using System.ComponentModel.DataAnnotations.Schema;

namespace BeverageBackend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.PendingPayment;
        [Column(TypeName ="decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
