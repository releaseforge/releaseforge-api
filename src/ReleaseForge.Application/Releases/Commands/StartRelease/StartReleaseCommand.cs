using MediatR;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.StartRelease;

public sealed record StartReleaseCommand(Guid Id) : IRequest<ReleaseDto>;
