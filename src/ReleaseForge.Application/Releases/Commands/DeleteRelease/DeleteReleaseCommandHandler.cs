using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Releases.Commands.DeleteRelease;

internal sealed class DeleteReleaseCommandHandler(
    IReleaseRepository releaseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteReleaseCommand>
{
    public async Task Handle(DeleteReleaseCommand request, CancellationToken cancellationToken)
    {
        var release = await releaseRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Release", request.Id);

        releaseRepository.Delete(release);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
