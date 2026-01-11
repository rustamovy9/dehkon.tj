using Application.DTO_s;
using FluentValidation;

namespace Application.Validation.Announcement;

public class CreateAnnouncementInfoValidator : AbstractValidator<AnnouncementCreateInfo>
{
    public CreateAnnouncementInfoValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty()
            .WithMessage("Title is required");
        
        RuleFor(c => c.Content)
            .NotEmpty()
            .WithMessage("Content is required");
    }
}