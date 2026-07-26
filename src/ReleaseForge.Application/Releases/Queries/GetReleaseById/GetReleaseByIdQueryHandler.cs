using MediatR;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Queries.GetReleaseById;

internal sealed class GetReleaseByIdQueryHandler(
    IReleaseRepository releaseRepository) : IRequestHandler<GetReleaseByIdQuery, ReleaseDto?>
{
    public async Task<ReleaseDto?> Handle(GetReleaseByIdQuery request, CancellationToken cancellationToken)
    {
        var release = await releaseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (release is null) return null;

        return new ReleaseDto(
            release.Id, release.ProjectId, release.Version, release.Title,
            release.Description, release.Status.ToString(), release.CreatedAt, release.ReleasedAt);
    }
}
