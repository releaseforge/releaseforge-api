using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Pipelines.Commands.DeletePipeline;

internal sealed class DeletePipelineCommandHandler(
    IPipelineRepository pipelineRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeletePipelineCommand>
{
    public async Task Handle(DeletePipelineCommand request, CancellationToken cancellationToken)
    {
        var pipeline = await pipelineRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Pipeline", request.Id);

        pipelineRepository.Delete(pipeline);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
