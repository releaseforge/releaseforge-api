using MediatR;
using ReleaseForge.Application.Deployments.Dto;

namespace ReleaseForge.Application.Deployments.Queries.GetDeploymentById;

public sealed record GetDeploymentByIdQuery(Guid Id) : IRequest<DeploymentDto>;
