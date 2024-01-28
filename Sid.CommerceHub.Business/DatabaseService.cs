using Data;
using Microsoft.EntityFrameworkCore;
using Sid.CommerceHub.Business.Models;

namespace Sid.CommerceHub.Business;

public class DatabaseService : IDatabaseService
{
    private readonly DatabaseContext _databaseContext;

    public DatabaseService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IEnumerable<ProductDomain>> GetProducts()
    {
        var productDatas = await _databaseContext.ProductData.ToListAsync();

        return productDatas.Select(product => new ProductDomain
        {
            ProductId = product.Id,
            Name = product.Name,
            Price = product.Price
        });
    }

    public async Task<IEnumerable<OrderDomain>> GetOrders()
    {
        var orderDatas = await _databaseContext.OrderData.ToListAsync();

        return orderDatas.Select(product => new OrderDomain
        {
            Id = product.Id,
            ProductId = product.Id,
            TotalPrice = product.TotalPrice,
            CreatedAt = product.CreatedAt
        });
    }

    public async Task<ProductDomain?> GetProductById(long productId)
    {
        var productData = await _databaseContext.ProductData.FindAsync(productId);

        if (productData != default)
        {
            var product = new ProductDomain
            {
                ProductId = productData.Id,
                Name = productData.Name,
                Price = productData.Price
            };

            return product;
        }

        return null;
    }

    public async Task<OrderDomain?> GetOrderById(long orderId)
    {
        var orderData = await _databaseContext.OrderData.FindAsync(orderId);

        if (orderData == default) return null;


        var orderDomain = new OrderDomain
        {
            Id = orderData.Id,
            ProductId = orderData.ProductId,
            TotalPrice = orderData.TotalPrice,
            CreatedAt = orderData.CreatedAt
        };

        return orderDomain;
    }

    public async Task CreateOrder(OrderCreateDomain orderCreateDomain)
    {
        await _databaseContext.OrderData.AddRangeAsync(new OrderData
        {
            ProductId = orderCreateDomain.ProductId,
            TotalPrice = orderCreateDomain.TotalPrice,
            CreatedAt = DateTime.UtcNow
        });

        await _databaseContext.SaveChangesAsync();
    }

    public async Task CreateProduct(ProductCreateDomain productCreateDomain)
    {
        await _databaseContext.ProductData.AddRangeAsync(new ProductData
        {
            Name = productCreateDomain.Name,
            Price = productCreateDomain.Price,
            CreatedAt = DateTime.UtcNow
        });

        await _databaseContext.SaveChangesAsync();
    }

    public async Task<ProductDomain?> GetProductByName(string productName)
    {
        try
        {
            var productData = await _databaseContext.ProductData
                .SingleOrDefaultAsync(data => data.Name.Equals(productName));

            if (productData == null)
                // Return null or throw an exception, depending on your design preference
                // You can throw an exception like NotFoundException to handle this case
                return null; // or throw new NotFoundException($"Product not found with name: {productName}");

            var product = new ProductDomain
            {
                ProductId = productData.Id,
                Name = productData.Name,
                Price = productData.Price
            };

            return product;
        }
        catch (Exception ex)
        {
            // Log the exception for further investigation
            // You may want to throw a custom exception or return null based on your design
            // If returning null, ensure the calling code handles the null case appropriately
            // You can also consider returning a Result<T> type that includes success/failure information
            // For simplicity, here, it returns null, but you might want to handle this differently
            return null;
        }
    }
}