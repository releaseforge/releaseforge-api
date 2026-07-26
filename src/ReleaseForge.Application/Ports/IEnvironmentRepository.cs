using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Application.Ports;

public interface IEnvironmentRepository
{
    Task<DeploymentEnvironment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<DeploymentEnvironment>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    void Add(DeploymentEnvironment environment);
    void Update(DeploymentEnvironment environment);
    void Delete(DeploymentEnvironment environment);
}
