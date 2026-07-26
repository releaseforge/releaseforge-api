using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Deployments.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Deployments.Commands.RollbackDeployment;

internal sealed class RollbackDeploymentCommandHandler(
    IDeploymentRepository deploymentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RollbackDeploymentCommand, DeploymentDto>
{
    public async Task<DeploymentDto> Handle(RollbackDeploymentCommand request, CancellationToken cancellationToken)
    {
        var deployment = await deploymentRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Deployment", request.Id);

        deployment.Rollback();

        deploymentRepository.Update(deployment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeploymentDto(
            deployment.Id, deployment.ReleaseId, deployment.EnvironmentId,
            deployment.Status.ToString(), deployment.CreatedAt,
            deployment.StartedAt, deployment.CompletedAt);
    }
}
