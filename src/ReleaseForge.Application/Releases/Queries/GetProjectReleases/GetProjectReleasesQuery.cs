using MediatR;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Queries.GetProjectReleases;

public sealed record GetProjectReleasesQuery(Guid ProjectId) : IRequest<List<ReleaseDto>>;
