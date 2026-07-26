using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Environments.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Environments.Commands.CreateEnvironment;

internal sealed class CreateEnvironmentCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateEnvironmentCommand, EnvironmentDto>
{
    public async Task<EnvironmentDto> Handle(CreateEnvironmentCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken)
            ?? throw new NotFoundException("Project", request.ProjectId);

        var env = project.CreateEnvironment(request.Name);

        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new EnvironmentDto(env.Id, env.ProjectId, env.Name, env.Slug, env.SortOrder);
    }
}
