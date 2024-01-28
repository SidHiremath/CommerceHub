namespace Sid.CommerceHub.Business.Models;

public class OrderCreateDomain
{
    public long ProductId { get; set; }
    public double TotalPrice { get; set; }
}