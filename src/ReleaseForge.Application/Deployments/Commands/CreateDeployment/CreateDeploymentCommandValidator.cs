using FluentValidation;

namespace ReleaseForge.Application.Deployments.Commands.CreateDeployment;

internal sealed class CreateDeploymentCommandValidator : AbstractValidator<CreateDeploymentCommand>
{
    public CreateDeploymentCommandValidator()
    {
        RuleFor(x => x.EnvironmentId).NotEmpty();
    }
}
