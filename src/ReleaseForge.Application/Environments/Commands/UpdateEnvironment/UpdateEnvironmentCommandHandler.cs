using MediatR;
using ReleaseForge.Application.Common.Exceptions;
using ReleaseForge.Application.Environments.Dto;
using ReleaseForge.Application.Ports;

namespace ReleaseForge.Application.Environments.Commands.UpdateEnvironment;

internal sealed class UpdateEnvironmentCommandHandler(
    IEnvironmentRepository environmentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateEnvironmentCommand, EnvironmentDto>
{
    public async Task<EnvironmentDto> Handle(UpdateEnvironmentCommand request, CancellationToken cancellationToken)
    {
        var env = await environmentRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Environment", request.Id);

        env.SetName(request.Name);
        env.SetSortOrder(request.SortOrder);

        environmentRepository.Update(env);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new EnvironmentDto(env.Id, env.ProjectId, env.Name, env.Slug, env.SortOrder);
    }
}
