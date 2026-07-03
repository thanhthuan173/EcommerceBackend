using EcommerceBackend.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Application.Dto.Cart
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
