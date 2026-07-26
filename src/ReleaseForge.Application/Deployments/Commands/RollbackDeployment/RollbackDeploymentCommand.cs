using MediatR;
using ReleaseForge.Application.Deployments.Dto;

namespace ReleaseForge.Application.Deployments.Commands.RollbackDeployment;

public sealed record RollbackDeploymentCommand(Guid Id) : IRequest<DeploymentDto>;
