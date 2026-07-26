using FluentValidation;

namespace ReleaseForge.Application.Releases.Commands.UpdateRelease;

internal sealed class UpdateReleaseCommandValidator : AbstractValidator<UpdateReleaseCommand>
{
    public UpdateReleaseCommandValidator()
    {
        RuleFor(x => x.Version).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}
