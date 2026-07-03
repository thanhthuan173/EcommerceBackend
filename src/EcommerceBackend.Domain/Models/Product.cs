using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Column("Price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public required string ImgUrl { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
