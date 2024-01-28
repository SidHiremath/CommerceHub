namespace Sid.CommerceHub.OrderService.Requests;

public class OrderCreateDto
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
}