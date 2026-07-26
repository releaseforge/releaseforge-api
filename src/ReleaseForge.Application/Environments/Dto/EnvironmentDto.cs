namespace ReleaseForge.Application.Environments.Dto;

public sealed record EnvironmentDto(Guid Id, Guid ProjectId, string Name, string Slug, int SortOrder);
