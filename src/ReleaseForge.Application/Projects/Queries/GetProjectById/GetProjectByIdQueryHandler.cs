using MediatR;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Projects.Dto;

namespace ReleaseForge.Application.Projects.Queries.GetProjectById;

internal sealed class GetProjectByIdQueryHandler(
    IProjectRepository projectRepository) : IRequestHandler<GetProjectByIdQuery, ProjectDetailDto?>
{
    public async Task<ProjectDetailDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
            return null;

        return new ProjectDetailDto(
            project.Id,
            project.Name,
            project.Slug,
            project.Description,
            project.CreatedAt,
            project.UpdatedAt,
            project.Releases.Count,
            project.Environments.Count,
            project.Pipelines.Count);
    }
}
