using Sid.CommerceHub.Business.Models;

namespace Sid.CommerceHub.Business;

public interface IDatabaseService
{
    public Task<IEnumerable<ProductDomain>> GetProducts();
    public Task<IEnumerable<OrderDomain>> GetOrders();
    public Task<ProductDomain?> GetProductById(long productId);
    public Task<ProductDomain?> GetProductByName(string productName);
    public Task<OrderDomain?> GetOrderById(long orderId);
    Task CreateOrder(OrderCreateDomain orderCreateDomain);
    Task CreateProduct(ProductCreateDomain productCreateDomain);
}