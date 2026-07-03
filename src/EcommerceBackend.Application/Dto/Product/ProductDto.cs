namespace EcommerceBackend.Application.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public required string ImgUrl { get; set; }

        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
