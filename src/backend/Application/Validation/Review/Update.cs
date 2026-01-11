using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Review;

public class UpdateReviewInfoValidator : AbstractValidator<ReviewUpdateInfo>
{
    public UpdateReviewInfoValidator()
    {
        RuleFor(r => r.Rating)
            .GreaterThan(0)
            .WithMessage("Rating is must be greater than 0")
            .InclusiveBetween(1,5);
        RuleFor(c => c.Comment)
            .MaximumLength(1000);
    }
}