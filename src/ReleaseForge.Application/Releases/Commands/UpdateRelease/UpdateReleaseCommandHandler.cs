using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.UpdateRelease;

internal sealed class UpdateReleaseCommandHandler(
    IReleaseRepository releaseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateReleaseCommand, ReleaseDto>
{
    public async Task<ReleaseDto> Handle(UpdateReleaseCommand request, CancellationToken cancellationToken)
    {
        var release = await releaseRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Release", request.Id);

        release.SetVersion(request.Version);
        release.SetTitle(request.Title);
        release.SetDescription(request.Description);

        releaseRepository.Update(release);
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
