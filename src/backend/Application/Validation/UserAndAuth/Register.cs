using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.UserAndAuth;

public class RegisterInfoValidator : AbstractValidator<RegisterRequest>
{
    public RegisterInfoValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format!!!");

        RuleFor(user => user.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(60).WithMessage("Full name must not exceed 60 characters.");

        RuleFor(user => user.UserName)
            .NotEmpty()
            .MaximumLength(40)
            .Matches("^[a-zA-Z0-9_]+$")
            .WithMessage("UserName can contain only letters, numbers and underscore");

        RuleFor(user => user.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+992\d{9}$")
            .WithMessage("Phone must be in format +992XXXXXXXXX");
        
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("The password must contain at least 8 characters.")
            .MaximumLength(50).WithMessage("Password must not exceed 50 characters.")
            .NotEqual(x=>x.UserName).WithMessage("Password cannot be the same as UserName");
        
        RuleFor(user => user.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm is required")
            .Equal(x=>x.Password).WithMessage("Passwords do not match");
    }
    
}