using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Chat;

public class CreatePrivateChatInfoValidator : AbstractValidator<PrivateChatCreateInfo>
{
    public CreatePrivateChatInfoValidator()
    {
        RuleFor(ch=>ch.OtherUserId)
            .GreaterThan(0)
            .WithMessage("User id must be greater than 0");
    }
}