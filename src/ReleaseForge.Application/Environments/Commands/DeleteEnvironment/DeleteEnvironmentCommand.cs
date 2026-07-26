using MediatR;

namespace ReleaseForge.Application.Environments.Commands.DeleteEnvironment;

public sealed record DeleteEnvironmentCommand(Guid Id) : IRequest;
