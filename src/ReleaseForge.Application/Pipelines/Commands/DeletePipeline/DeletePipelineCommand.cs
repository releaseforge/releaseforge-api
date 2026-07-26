using MediatR;

namespace ReleaseForge.Application.Pipelines.Commands.DeletePipeline;

public sealed record DeletePipelineCommand(Guid Id) : IRequest;
