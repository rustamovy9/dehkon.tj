using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Order;

public class CreateOrderInfoValidator : AbstractValidator<OrderCreateInfo>
{
    public CreateOrderInfoValidator()
    {
        RuleFor(d => d.DeliveryAddress)
            .MaximumLength(100)
            .When(a => !string.IsNullOrEmpty(a.DeliveryAddress));
    }
}