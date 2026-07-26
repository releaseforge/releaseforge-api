using Microsoft.EntityFrameworkCore;
using ReleaseForge.Application.Ports;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Repositories;

internal sealed class EnvironmentRepository(AppDbContext context) : IEnvironmentRepository
{
    public async Task<DeploymentEnvironment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Environments
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<List<DeploymentEnvironment>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await context.Environments
            .Where(e => e.ProjectId == projectId)
            .OrderBy(e => e.SortOrder)
            .ToListAsync(cancellationToken);
    }

    public void Add(DeploymentEnvironment environment)
    {
        context.Environments.Add(environment);
    }

    public void Update(DeploymentEnvironment environment)
    {
        context.Environments.Update(environment);
    }

    public void Delete(DeploymentEnvironment environment)
    {
        context.Environments.Remove(environment);
    }
}
