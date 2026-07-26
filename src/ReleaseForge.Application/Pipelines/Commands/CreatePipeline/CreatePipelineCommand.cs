using MediatR;
using ReleaseForge.Application.Pipelines.Dto;

namespace ReleaseForge.Application.Pipelines.Commands.CreatePipeline;

public sealed record CreatePipelineCommand(Guid ProjectId, string Name) : IRequest<PipelineDto>;
