using MediatR;
using ReleaseForge.Application.Deployments.Dto;

namespace ReleaseForge.Application.Deployments.Commands.SucceedDeployment;

public sealed record SucceedDeploymentCommand(Guid Id) : IRequest<DeploymentDto>;
