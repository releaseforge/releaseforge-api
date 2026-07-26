using MediatR;
using ReleaseForge.Application.Pipelines.Dto;

namespace ReleaseForge.Application.Pipelines.Commands.UpdatePipeline;

public sealed record UpdatePipelineCommand(Guid Id, string Name) : IRequest<PipelineDto>;
