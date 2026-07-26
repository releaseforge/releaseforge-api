using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Pipelines.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Pipelines.Commands.CreatePipeline;

internal sealed class CreatePipelineCommandHandler(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreatePipelineCommand, PipelineDto>
{
    public async Task<PipelineDto> Handle(CreatePipelineCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken)
            ?? throw new NotFoundException("Project", request.ProjectId);

        var pipeline = project.CreatePipeline(request.Name);

        projectRepository.Update(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new PipelineDto(pipeline.Id, pipeline.ProjectId, pipeline.Name, pipeline.CreatedAt);
    }
}
