using EcommerceBackend.Domain.Enums;

namespace EcommerceBackend.Application.Common.Query
{
    public class OrderQueryParameters:QueryParameters
    {
        public decimal? MinTotalAmount { get; set; }
        public decimal? MaxTotalAmount { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
