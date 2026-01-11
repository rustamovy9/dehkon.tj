using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.UserAndAuth;

public class AdminChangeUserRoleInfoValidator : AbstractValidator<AdminChangeUserRoleInfo>
{
    public AdminChangeUserRoleInfoValidator()
    {
        RuleFor(role => role.RoleId)
            .GreaterThan(0)
            .WithMessage("RoleId must be greater than 0");
    }
}