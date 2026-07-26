using MediatR;
using ReleaseForge.Application.Ports;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Queries.GetProjectReleases;

internal sealed class GetProjectReleasesQueryHandler(
    IReleaseRepository releaseRepository) : IRequestHandler<GetProjectReleasesQuery, List<ReleaseDto>>
{
    public async Task<List<ReleaseDto>> Handle(GetProjectReleasesQuery request, CancellationToken cancellationToken)
    {
        var releases = await releaseRepository.GetByProjectIdAsync(request.ProjectId, cancellationToken);

        return releases
            .Select(r => new ReleaseDto(
                r.Id, r.ProjectId, r.Version, r.Title, r.Description,
                r.Status.ToString(), r.CreatedAt, r.ReleasedAt))
            .ToList();
    }
}
