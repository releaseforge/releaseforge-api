using MediatR;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.PlanRelease;

public sealed record PlanReleaseCommand(Guid Id) : IRequest<ReleaseDto>;
