using FluentValidation;

namespace ReleaseForge.Application.Releases.Commands.CreateRelease;

internal sealed class CreateReleaseCommandValidator : AbstractValidator<CreateReleaseCommand>
{
    public CreateReleaseCommandValidator()
    {
        RuleFor(x => x.Version).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}
