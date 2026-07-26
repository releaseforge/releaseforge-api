using FluentValidation;

namespace ReleaseForge.Application.Environments.Commands.CreateEnvironment;

internal sealed class CreateEnvironmentCommandValidator : AbstractValidator<CreateEnvironmentCommand>
{
    public CreateEnvironmentCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
