using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Projects.Commands.DeleteProject;

internal sealed class DeleteProjectCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Project", request.Id);

        projectRepository.Delete(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
