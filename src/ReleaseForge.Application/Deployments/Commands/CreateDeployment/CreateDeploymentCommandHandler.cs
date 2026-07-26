using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Deployments.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Deployments.Commands.CreateDeployment;

internal sealed class CreateDeploymentCommandHandler(
    IReleaseRepository releaseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateDeploymentCommand, DeploymentDto>
{
    public async Task<DeploymentDto> Handle(CreateDeploymentCommand request, CancellationToken cancellationToken)
    {
        var release = await releaseRepository.GetByIdAsync(request.ReleaseId, cancellationToken)
            ?? throw new NotFoundException("Release", request.ReleaseId);

        var deployment = release.CreateDeployment(request.EnvironmentId);

        releaseRepository.Update(release);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeploymentDto(
            deployment.Id, deployment.ReleaseId, deployment.EnvironmentId,
            deployment.Status.ToString(), deployment.CreatedAt,
            deployment.StartedAt, deployment.CompletedAt);
    }
}
