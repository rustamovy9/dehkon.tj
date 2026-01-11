using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Category;

public class CreateInfoValidator : AbstractValidator<CategoryCreateInfo>
{
    public CreateInfoValidator()
    {
        RuleFor(n => n.Name)
            .NotEmpty()
            .WithMessage("Category name is required");
    }
}