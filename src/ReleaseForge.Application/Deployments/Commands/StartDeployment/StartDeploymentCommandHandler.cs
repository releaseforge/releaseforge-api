using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Deployments.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Deployments.Commands.StartDeployment;

internal sealed class StartDeploymentCommandHandler(
    IDeploymentRepository deploymentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<StartDeploymentCommand, DeploymentDto>
{
    public async Task<DeploymentDto> Handle(StartDeploymentCommand request, CancellationToken cancellationToken)
    {
        var deployment = await deploymentRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Deployment", request.Id);

        deployment.Start();

        deploymentRepository.Update(deployment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeploymentDto(
            deployment.Id, deployment.ReleaseId, deployment.EnvironmentId,
            deployment.Status.ToString(), deployment.CreatedAt,
            deployment.StartedAt, deployment.CompletedAt);
    }
}
