using FluentValidation;

namespace ReleaseForge.Application.Pipelines.Commands.UpdatePipeline;

internal sealed class UpdatePipelineCommandValidator : AbstractValidator<UpdatePipelineCommand>
{
    public UpdatePipelineCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
