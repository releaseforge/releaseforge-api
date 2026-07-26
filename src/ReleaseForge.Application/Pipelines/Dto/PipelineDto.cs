namespace ReleaseForge.Application.Pipelines.Dto;

public sealed record PipelineDto(Guid Id, Guid ProjectId, string Name, DateTime CreatedAt);
