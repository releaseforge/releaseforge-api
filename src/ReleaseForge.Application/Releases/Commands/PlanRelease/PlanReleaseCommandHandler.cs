using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.PlanRelease;

internal sealed class PlanReleaseCommandHandler(
    IReleaseRepository releaseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<PlanReleaseCommand, ReleaseDto>
{
    public async Task<ReleaseDto> Handle(PlanReleaseCommand request, CancellationToken cancellationToken)
    {
        var release = await releaseRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Release", request.Id);

        release.Plan();
        releaseRepository.Update(release);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReleaseDto(
            release.Id, release.ProjectId, release.Version, release.Title,
            release.Description, release.Status.ToString(), release.CreatedAt, release.ReleasedAt);
    }
}
