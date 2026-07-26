namespace ReleaseForge.Application.Projects.Dto;

public sealed record ProjectDetailDto(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int ReleaseCount,
    int EnvironmentCount,
    int PipelineCount);
