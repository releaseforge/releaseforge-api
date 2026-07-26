using FluentValidation;

namespace ReleaseForge.Application.Environments.Commands.UpdateEnvironment;

internal sealed class UpdateEnvironmentCommandValidator : AbstractValidator<UpdateEnvironmentCommand>
{
    public UpdateEnvironmentCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
