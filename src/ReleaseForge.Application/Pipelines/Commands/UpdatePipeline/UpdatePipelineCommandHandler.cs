using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Pipelines.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Pipelines.Commands.UpdatePipeline;

internal sealed class UpdatePipelineCommandHandler(
    IPipelineRepository pipelineRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdatePipelineCommand, PipelineDto>
{
    public async Task<PipelineDto> Handle(UpdatePipelineCommand request, CancellationToken cancellationToken)
    {
        var pipeline = await pipelineRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Pipeline", request.Id);

        pipeline.SetName(request.Name);

        pipelineRepository.Update(pipeline);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new PipelineDto(pipeline.Id, pipeline.ProjectId, pipeline.Name, pipeline.CreatedAt);
    }
}
