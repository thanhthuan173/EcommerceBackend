namespace EcommerceBackend.Application.Common.Query
{
    public class ProductQueryParameters:QueryParameters
    {
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
    }
}
