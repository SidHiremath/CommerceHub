using FluentValidation;
using Sid.CommerceHub.OrderService.Requests;

namespace Sid.CommerceHub.OrderService.Validators;

public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateValidator()
    {
        RuleFor(create => create.ProductId)
            .NotNull().WithMessage("ProductDomain ID cannot be null.")
            .GreaterThan(0).WithMessage("ProductDomain ID must be greater than 0.");

        RuleFor(create => create.Quantity)
            .NotNull().WithMessage("Quantity cannot be null.")
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Quantity must be 100 or less."); // Adjust the upper limit as needed
    }
}