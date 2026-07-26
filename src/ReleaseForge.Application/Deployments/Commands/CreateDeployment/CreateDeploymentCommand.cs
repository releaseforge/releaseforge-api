using MediatR;
using ReleaseForge.Application.Deployments.Dto;

namespace ReleaseForge.Application.Deployments.Commands.CreateDeployment;

public sealed record CreateDeploymentCommand(Guid ReleaseId, Guid EnvironmentId) : IRequest<DeploymentDto>;
