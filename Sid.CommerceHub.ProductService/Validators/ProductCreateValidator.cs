using FluentValidation;
using Sid.CommerceHub.ProductService.Dtos;

namespace Sid.CommerceHub.ProductService.Validators;

public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty();
        RuleFor(dto => dto.Price).NotEmpty();
    }
}