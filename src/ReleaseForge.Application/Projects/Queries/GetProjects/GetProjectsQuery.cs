using MediatR;
using ReleaseForge.Application.Projects.Dto;

namespace ReleaseForge.Application.Projects.Queries.GetProjects;

public sealed record GetProjectsQuery : IRequest<List<ProjectDto>>;
