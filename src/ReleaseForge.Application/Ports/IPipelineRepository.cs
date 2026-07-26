using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Application.Ports;

public interface IPipelineRepository
{
    Task<Pipeline?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Pipeline>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    void Add(Pipeline pipeline);
    void Update(Pipeline pipeline);
    void Delete(Pipeline pipeline);
}
