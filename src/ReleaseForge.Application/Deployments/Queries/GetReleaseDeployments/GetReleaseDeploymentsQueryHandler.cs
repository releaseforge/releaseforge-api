using MediatR;
using ReleaseForge.Application.Deployments.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Deployments.Queries.GetReleaseDeployments;

internal sealed class GetReleaseDeploymentsQueryHandler(
    IDeploymentRepository deploymentRepository) : IRequestHandler<GetReleaseDeploymentsQuery, List<DeploymentDto>>
{
    public async Task<List<DeploymentDto>> Handle(GetReleaseDeploymentsQuery request, CancellationToken cancellationToken)
    {
        var deployments = await deploymentRepository.GetByReleaseIdAsync(request.ReleaseId, cancellationToken);

        return deployments
            .Select(d => new DeploymentDto(
                d.Id, d.ReleaseId, d.EnvironmentId, d.Status.ToString(),
                d.CreatedAt, d.StartedAt, d.CompletedAt))
            .ToList();
    }
}
