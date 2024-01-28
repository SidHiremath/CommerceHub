using Microsoft.AspNetCore.Mvc;
using Sid.CommerceHub.Business;
using Sid.CommerceHub.Business.Models;
using Sid.CommerceHub.ProductService.Dtos;

namespace Sid.CommerceHub.ProductService;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public ProductsController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _databaseService.GetProducts();

        return Ok(products.Select(productDomain => new ProductDto
        {
            Id = productDomain.ProductId,
            Name = productDomain.Name,
            Price = productDomain.Price,
            CreatedAt = productDomain.CreatedAt
        }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var productDomain = await _databaseService.GetProductById(id);

        if (productDomain == null) return new NotFoundResult();

        return Ok(new ProductDto
        {
            Id = productDomain.ProductId,
            Name = productDomain.Name,
            Price = productDomain.Price,
            CreatedAt = productDomain.CreatedAt
        });
    }


    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto)
    {
        var productDetails = await _databaseService.GetProductByName(productCreateDto.Name);

        if (productDetails == null)
        {
            var order = new ProductCreateDomain
            {
                Name = productCreateDto.Name,
                Price = productCreateDto.Price
            };

            await _databaseService.CreateProduct(order);
            return Ok(order);
        }

        return NotFound($"Product already found with name : {productCreateDto.Name}");
    }
}