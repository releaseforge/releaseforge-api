using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Deployments.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Deployments.Queries.GetDeploymentById;

internal sealed class GetDeploymentByIdQueryHandler(
    IDeploymentRepository deploymentRepository) : IRequestHandler<GetDeploymentByIdQuery, DeploymentDto>
{
    public async Task<DeploymentDto> Handle(GetDeploymentByIdQuery request, CancellationToken cancellationToken)
    {
        var deployment = await deploymentRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Deployment", request.Id);

        return new DeploymentDto(
            deployment.Id, deployment.ReleaseId, deployment.EnvironmentId,
            deployment.Status.ToString(), deployment.CreatedAt,
            deployment.StartedAt, deployment.CompletedAt);
    }
}
