using MediatR;

namespace ReleaseForge.Application.Projects.Commands.DeleteProject;

public sealed record DeleteProjectCommand(Guid Id) : IRequest;
