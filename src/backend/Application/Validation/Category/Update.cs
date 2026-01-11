using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Category;

public class UpdateInfoValidator : AbstractValidator<CategoryUpdateInfo>
{
    public UpdateInfoValidator()
    {
        RuleFor(n => n.Name)
            .NotEmpty()
            .WithMessage("Category name is required");
    }
}