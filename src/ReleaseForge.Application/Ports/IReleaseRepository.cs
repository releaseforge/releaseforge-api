using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Application.Ports;

public interface IReleaseRepository
{
    Task<Release?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Release>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    void Add(Release release);
    void Update(Release release);
    void Delete(Release release);
}
