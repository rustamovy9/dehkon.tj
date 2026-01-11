using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.UserAndAuth;

public class LoginInfoValidator : AbstractValidator<LoginRequest>
{
    public LoginInfoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must contain at least 8 characters.")
            .MaximumLength(50).WithMessage("Password must not exceed 50 characters.");
    }
}