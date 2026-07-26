using FluentValidation;

namespace ReleaseForge.Application.Pipelines.Commands.CreatePipeline;

internal sealed class CreatePipelineCommandValidator : AbstractValidator<CreatePipelineCommand>
{
    public CreatePipelineCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
