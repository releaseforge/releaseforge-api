using MediatR;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.CancelRelease;

public sealed record CancelReleaseCommand(Guid Id) : IRequest<ReleaseDto>;
