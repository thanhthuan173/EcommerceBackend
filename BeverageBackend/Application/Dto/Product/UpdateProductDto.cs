namespace BeverageBackend.Application.Dto.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImgUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
