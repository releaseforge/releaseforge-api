using MediatR;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.DeployRelease;

public sealed record DeployReleaseCommand(Guid Id) : IRequest<ReleaseDto>;
