using MediatR;
using ReleaseForge.Application.Deployments.Dto;

namespace ReleaseForge.Application.Deployments.Commands.CancelDeployment;

public sealed record CancelDeploymentCommand(Guid Id) : IRequest<DeploymentDto>;
