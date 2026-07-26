using MediatR;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Projects.Dto;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Application.Projects.Commands.CreateProject;

internal sealed class CreateProjectCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Name, request.Description);

        projectRepository.Add(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProjectDto(
            project.Id,
            project.Name,
            project.Slug,
            project.Description,
            project.CreatedAt,
            project.UpdatedAt);
    }
}
