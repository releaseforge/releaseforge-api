using MediatR;
using ReleaseForge.Application.Environments.Dto;

namespace ReleaseForge.Application.Environments.Commands.UpdateEnvironment;

public sealed record UpdateEnvironmentCommand(Guid Id, string Name, int SortOrder) : IRequest<EnvironmentDto>;
