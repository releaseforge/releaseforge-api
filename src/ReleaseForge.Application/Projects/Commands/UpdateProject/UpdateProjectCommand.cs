using MediatR;
using ReleaseForge.Application.Projects.Dto;

namespace ReleaseForge.Application.Projects.Commands.UpdateProject;

public sealed record UpdateProjectCommand(Guid Id, string Name, string? Description) : IRequest<ProjectDto>;
