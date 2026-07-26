using MediatR;
using ReleaseForge.Application.Deployments.Dto;

namespace ReleaseForge.Application.Deployments.Queries.GetReleaseDeployments;

public sealed record GetReleaseDeploymentsQuery(Guid ReleaseId) : IRequest<List<DeploymentDto>>;
