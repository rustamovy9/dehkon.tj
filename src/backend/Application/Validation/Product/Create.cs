using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Product;

public class CreateProductInfoValidator : AbstractValidator<ProductCreateInfo>
{
    public CreateProductInfoValidator()
    {
        RuleFor(n => n.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .MaximumLength(50);

        RuleFor(i => i.ImageUrl)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.ImageUrl));

        RuleFor(p=>p.PricePerKg)
            .GreaterThan(0)
            .WithMessage("Price per kg is must be greater than 0");
        
        RuleFor(s=>s.StockPerKg)
            .GreaterThan(0)
            .WithMessage("Stock per kg is must be greater than 0");

        RuleFor(c => c.CategoryId)
            .GreaterThan(0)
            .WithMessage("Category id is must be greater than 0");
    }
}