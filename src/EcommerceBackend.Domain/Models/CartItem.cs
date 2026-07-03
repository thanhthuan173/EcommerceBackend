using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Domain.Models
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
