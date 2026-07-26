using MediatR;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.CreateRelease;

public sealed record CreateReleaseCommand(Guid ProjectId, string Version, string Title, string? Description)
    : IRequest<ReleaseDto>;
