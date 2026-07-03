namespace EcommerceBackend.Application.Dto.Product
{
    public class CategoryWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
