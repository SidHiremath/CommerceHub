namespace Sid.CommerceHub.Business.Models;

public class OrderDomain
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public double TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}