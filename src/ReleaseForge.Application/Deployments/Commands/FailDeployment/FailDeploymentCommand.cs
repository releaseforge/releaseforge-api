using MediatR;
using ReleaseForge.Application.Deployments.Dto;

namespace ReleaseForge.Application.Deployments.Commands.FailDeployment;

public sealed record FailDeploymentCommand(Guid Id) : IRequest<DeploymentDto>;
