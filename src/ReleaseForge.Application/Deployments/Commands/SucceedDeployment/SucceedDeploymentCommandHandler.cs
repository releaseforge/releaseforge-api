using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Deployments.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Deployments.Commands.SucceedDeployment;

internal sealed class SucceedDeploymentCommandHandler(
    IDeploymentRepository deploymentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<SucceedDeploymentCommand, DeploymentDto>
{
    public async Task<DeploymentDto> Handle(SucceedDeploymentCommand request, CancellationToken cancellationToken)
    {
        var deployment = await deploymentRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Deployment", request.Id);

        deployment.Succeed();

        deploymentRepository.Update(deployment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeploymentDto(
            deployment.Id, deployment.ReleaseId, deployment.EnvironmentId,
            deployment.Status.ToString(), deployment.CreatedAt,
            deployment.StartedAt, deployment.CompletedAt);
    }
}
