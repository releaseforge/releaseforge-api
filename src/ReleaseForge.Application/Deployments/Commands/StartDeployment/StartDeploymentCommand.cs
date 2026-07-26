using MediatR;
using ReleaseForge.Application.Deployments.Dto;

namespace ReleaseForge.Application.Deployments.Commands.StartDeployment;

public sealed record StartDeploymentCommand(Guid Id) : IRequest<DeploymentDto>;
