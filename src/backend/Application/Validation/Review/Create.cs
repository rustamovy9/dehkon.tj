using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Review;

public class CreateReviewInfoValidator : AbstractValidator<ReviewCreateInfo>
{
    public CreateReviewInfoValidator()
    {
        RuleFor(p=>p.ProductId)
            .GreaterThan(0)
            .WithMessage("Product id is must be greater than 0");

        RuleFor(r => r.Rating)
            .GreaterThan(0)
            .WithMessage("Rating is must be greater than 0")
            .InclusiveBetween(1,5);
        RuleFor(c => c.Comment)
            .MaximumLength(1000);
    }
}