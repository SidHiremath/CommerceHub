using Microsoft.AspNetCore.Mvc;
using Sid.CommerceHub.Business;
using Sid.CommerceHub.Business.Models;
using Sid.CommerceHub.OrderService.Dtos;
using Sid.CommerceHub.OrderService.Requests;

namespace Sid.CommerceHub.OrderService;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public OrdersController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>> GetAll()
    {
        var orderDomains = await _databaseService.GetOrders();

        return orderDomains.Select(orderDomain => new OrderDto
        {
            Id = orderDomain.Id,
            ProductId = orderDomain.ProductId,
            TotalPrice = orderDomain.TotalPrice,
            CreatedAt = orderDomain.CreatedAt
        });
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> Get(long orderId)
    {
        var order = await _databaseService.GetOrderById(orderId);

        if (order == null)
            return NotFound($"Order not found with orderId : {orderId}");

        return Ok(new OrderDto
        {
            ProductId = order.ProductId,
            Id = order.Id,
            TotalPrice = order.TotalPrice,
            CreatedAt = order.CreatedAt
        });
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(OrderCreateDto orderCreateDto)
    {
        var productDetails = await _databaseService.GetProductById(orderCreateDto.ProductId);

        if (productDetails != null)
        {
            var order = new OrderCreateDomain
            {
                ProductId = orderCreateDto.ProductId,
                TotalPrice = productDetails.Price * orderCreateDto.Quantity
            };

            await _databaseService.CreateOrder(order);
            return Ok(order);
        }

        return NotFound($"Product not found with productId : {orderCreateDto.ProductId}");
    }
}