using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.CartItem;

public class UpdateCartItemInfoValidator : AbstractValidator<CartItemUpdateInfo>
{
    public UpdateCartItemInfoValidator()
    {
        RuleFor(p=>p.ProductId)
            .GreaterThan(0)
            .WithMessage("Product id must be greater than 0");

        RuleFor(q => q.QuantityKg)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0")
            .LessThanOrEqualTo(1000)
            .WithMessage("Quantity is too large");
    }
}