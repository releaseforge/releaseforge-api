using MediatR;
using ReleaseForge.Application.Environments.Dto;

namespace ReleaseForge.Application.Environments.Commands.CreateEnvironment;

public sealed record CreateEnvironmentCommand(Guid ProjectId, string Name) : IRequest<EnvironmentDto>;
