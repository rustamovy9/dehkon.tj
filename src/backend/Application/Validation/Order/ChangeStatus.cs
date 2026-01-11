using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Order;

public class ChangeOrderStatusInfoValidator : AbstractValidator<ChangeOrderStatusInfo>
{
    public ChangeOrderStatusInfoValidator()
    {
        RuleFor(s => s.Status)
            .IsInEnum()
            .WithMessage("Invalid order status");
    }
}