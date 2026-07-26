using MediatR;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Projects.Dto;

namespace ReleaseForge.Application.Projects.Queries.GetProjects;

internal sealed class GetProjectsQueryHandler(
    IProjectRepository projectRepository) : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    public async Task<List<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAllAsync(cancellationToken);

        return projects
            .Select(p => new ProjectDto(p.Id, p.Name, p.Slug, p.Description, p.CreatedAt, p.UpdatedAt))
            .ToList();
    }
}
