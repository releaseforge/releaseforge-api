using Microsoft.EntityFrameworkCore;
using ReleaseForge.Application.Ports;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Repositories;

internal sealed class DeploymentRepository(AppDbContext context) : IDeploymentRepository
{
    public async Task<Deployment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Deployments
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<List<Deployment>> GetByReleaseIdAsync(Guid releaseId, CancellationToken cancellationToken = default)
    {
        return await context.Deployments
            .Where(d => d.ReleaseId == releaseId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public void Add(Deployment deployment)
    {
        context.Deployments.Add(deployment);
    }

    public void Update(Deployment deployment)
    {
        context.Deployments.Update(deployment);
    }

    public void Delete(Deployment deployment)
    {
        context.Deployments.Remove(deployment);
    }
}
