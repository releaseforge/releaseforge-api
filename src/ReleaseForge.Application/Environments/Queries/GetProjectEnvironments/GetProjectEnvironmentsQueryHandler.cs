using MediatR;
using ReleaseForge.Application.Environments.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Environments.Queries.GetProjectEnvironments;

internal sealed class GetProjectEnvironmentsQueryHandler(
    IEnvironmentRepository environmentRepository) : IRequestHandler<GetProjectEnvironmentsQuery, List<EnvironmentDto>>
{
    public async Task<List<EnvironmentDto>> Handle(GetProjectEnvironmentsQuery request, CancellationToken cancellationToken)
    {
        var environments = await environmentRepository.GetByProjectIdAsync(request.ProjectId, cancellationToken);

        return environments
            .Select(e => new EnvironmentDto(e.Id, e.ProjectId, e.Name, e.Slug, e.SortOrder))
            .ToList();
    }
}
