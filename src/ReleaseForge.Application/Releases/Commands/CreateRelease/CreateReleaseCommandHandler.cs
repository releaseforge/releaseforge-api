using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Releases.Dto;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Application.Releases.Commands.CreateRelease;

internal sealed class CreateReleaseCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateReleaseCommand, ReleaseDto>
{
    public async Task<ReleaseDto> Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken)
            ?? throw new NotFoundException("Project", request.ProjectId);

        var release = project.CreateRelease(request.Version, request.Title, request.Description);

        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReleaseDto(
            release.Id,
            release.ProjectId,
            release.Version,
            release.Title,
            release.Description,
            release.Status.ToString(),
            release.CreatedAt,
            release.ReleasedAt);
    }
}
