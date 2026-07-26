using MediatR;
using ReleaseForge.Application.Releases.Dto;

namespace ReleaseForge.Application.Releases.Commands.RollbackRelease;

public sealed record RollbackReleaseCommand(Guid Id) : IRequest<ReleaseDto>;
