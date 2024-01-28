namespace Sid.CommerceHub.ProductService.Dtos;

public class ProductDto
{
    public long Id { set; get; }
    public string Name { set; get; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
}