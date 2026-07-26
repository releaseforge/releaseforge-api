using MediatR;

namespace ReleaseForge.Application.Releases.Commands.DeleteRelease;

public sealed record DeleteReleaseCommand(Guid Id) : IRequest;
