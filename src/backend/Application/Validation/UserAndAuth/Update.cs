using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.UserAndAuth;

public class UpdateUserInfoValidator : AbstractValidator<UserUpdateInfo>
{
    public UpdateUserInfoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(40)
            .Matches("^[a-zA-Z0-9_]+$")
            .WithMessage("UserName can contain only letters, numbers and underscore");
        
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(60);
        
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+992\d{9}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Phone must be in format +992XXXXXXXXX");
    }
}