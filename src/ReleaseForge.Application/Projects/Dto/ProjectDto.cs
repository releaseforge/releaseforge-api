namespace ReleaseForge.Application.Projects.Dto;

public sealed record ProjectDto(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
