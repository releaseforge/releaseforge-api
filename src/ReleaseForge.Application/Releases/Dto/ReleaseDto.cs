namespace ReleaseForge.Application.Releases.Dto;

public sealed record ReleaseDto(
    Guid Id,
    Guid ProjectId,
    string Version,
    string Title,
    string? Description,
    string Status,
    DateTime CreatedAt,
    DateTime? ReleasedAt);
