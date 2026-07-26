using MediatR;
using ReleaseForge.Application.Projects.Dto;

namespace ReleaseForge.Application.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand(string Name, string? Description) : IRequest<ProjectDto>;
