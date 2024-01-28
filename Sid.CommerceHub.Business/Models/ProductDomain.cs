namespace Sid.CommerceHub.Business.Models;

public class ProductDomain
{
    public long ProductId { set; get; }
    public string Name { set; get; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
}