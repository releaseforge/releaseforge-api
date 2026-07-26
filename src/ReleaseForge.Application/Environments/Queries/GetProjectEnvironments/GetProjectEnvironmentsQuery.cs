using MediatR;
using ReleaseForge.Application.Environments.Dto;

namespace ReleaseForge.Application.Environments.Queries.GetProjectEnvironments;

public sealed record GetProjectEnvironmentsQuery(Guid ProjectId) : IRequest<List<EnvironmentDto>>;
