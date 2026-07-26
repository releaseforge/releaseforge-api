using MediatR;
using ReleaseForge.Application.Projects.Dto;

namespace ReleaseForge.Application.Projects.Queries.GetProjectById;

public sealed record GetProjectByIdQuery(Guid Id) : IRequest<ProjectDetailDto?>;
