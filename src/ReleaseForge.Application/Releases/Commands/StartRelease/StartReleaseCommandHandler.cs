using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.StartRelease;

internal sealed class StartReleaseCommandHandler(
    IReleaseRepository releaseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<StartReleaseCommand, ReleaseDto>
{
    public async Task<ReleaseDto> Handle(StartReleaseCommand request, CancellationToken cancellationToken)
    {
        var release = await releaseRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Release", request.Id);

        release.Start();
        releaseRepository.Update(release);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReleaseDto(
            release.Id, release.ProjectId, release.Version, release.Title,
            release.Description, release.Status.ToString(), release.CreatedAt, release.ReleasedAt);
    }
}
