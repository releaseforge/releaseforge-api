using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Projects.Dto;

namespace ReleaseForge.Application.Projects.Commands.UpdateProject;

internal sealed class UpdateProjectCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Project", request.Id);

        project.SetName(request.Name);
        project.SetDescription(request.Description);

        projectRepository.Update(project);
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
