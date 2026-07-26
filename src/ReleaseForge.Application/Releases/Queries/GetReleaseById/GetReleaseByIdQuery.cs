using MediatR;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Queries.GetReleaseById;

public sealed record GetReleaseByIdQuery(Guid Id) : IRequest<ReleaseDto?>;
