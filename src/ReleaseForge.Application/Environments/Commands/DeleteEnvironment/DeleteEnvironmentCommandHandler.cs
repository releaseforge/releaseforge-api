using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Environments.Commands.DeleteEnvironment;

internal sealed class DeleteEnvironmentCommandHandler(
    IEnvironmentRepository environmentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteEnvironmentCommand>
{
    public async Task Handle(DeleteEnvironmentCommand request, CancellationToken cancellationToken)
    {
        var env = await environmentRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Environment", request.Id);

        environmentRepository.Delete(env);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
