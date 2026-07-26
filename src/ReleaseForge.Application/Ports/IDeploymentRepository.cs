using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Application.Ports;

public interface IDeploymentRepository
{
    Task<Deployment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Deployment>> GetByReleaseIdAsync(Guid releaseId, CancellationToken cancellationToken = default);
    void Add(Deployment deployment);
    void Update(Deployment deployment);
    void Delete(Deployment deployment);
}
