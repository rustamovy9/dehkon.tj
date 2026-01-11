using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Message;

public class CreateMessageInfoValidator : AbstractValidator<MessageCreateInfo>
{
    public CreateMessageInfoValidator()
    {
        RuleFor(c=>c.ChatId)
            .GreaterThan(0)
            .WithMessage("Chat id is must be grater than 0");

        RuleFor(t => t.Text)
            .NotEmpty()
            .WithMessage("Text is required.")
            .MaximumLength(2000)
            .WithMessage("Message is too long");
    }
}