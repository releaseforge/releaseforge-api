namespace ReleaseForge.Application.Deployments.Dto;

public sealed record DeploymentDto(
    Guid Id,
    Guid ReleaseId,
    Guid EnvironmentId,
    string Status,
    DateTime CreatedAt,
    DateTime? StartedAt,
    DateTime? CompletedAt);
