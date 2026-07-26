using MediatR;
using ReleaseForge.Application.Pipelines.Dto;

namespace ReleaseForge.Application.Pipelines.Queries.GetProjectPipelines;

public sealed record GetProjectPipelinesQuery(Guid ProjectId) : IRequest<List<PipelineDto>>;
