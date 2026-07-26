using MediatR;
using ReleaseForge.Application.Pipelines.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Pipelines.Queries.GetProjectPipelines;

internal sealed class GetProjectPipelinesQueryHandler(
    IPipelineRepository pipelineRepository) : IRequestHandler<GetProjectPipelinesQuery, List<PipelineDto>>
{
    public async Task<List<PipelineDto>> Handle(GetProjectPipelinesQuery request, CancellationToken cancellationToken)
    {
        var pipelines = await pipelineRepository.GetByProjectIdAsync(request.ProjectId, cancellationToken);

        return pipelines
            .Select(p => new PipelineDto(p.Id, p.ProjectId, p.Name, p.CreatedAt))
            .ToList();
    }
}
