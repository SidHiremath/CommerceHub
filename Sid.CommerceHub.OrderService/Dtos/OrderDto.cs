namespace Sid.CommerceHub.OrderService.Dtos;

public class OrderDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public double TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}