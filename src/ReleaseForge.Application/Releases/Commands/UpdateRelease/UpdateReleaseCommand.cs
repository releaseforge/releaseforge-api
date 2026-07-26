using MediatR;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.UpdateRelease;

public sealed record UpdateReleaseCommand(Guid Id, string Version, string Title, string? Description)
    : IRequest<ReleaseDto>;
