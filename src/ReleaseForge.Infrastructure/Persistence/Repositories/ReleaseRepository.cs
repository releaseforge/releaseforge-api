using Microsoft.EntityFrameworkCore;
using ReleaseForge.Application.Ports;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Repositories;

internal sealed class ReleaseRepository(AppDbContext context) : IReleaseRepository
{
    public async Task<Release?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Releases
            .Include(r => r.Deployments)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<List<Release>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await context.Releases
            .Where(r => r.ProjectId == projectId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public void Add(Release release)
    {
        context.Releases.Add(release);
    }

    public void Update(Release release)
    {
        context.Releases.Update(release);
    }

    public void Delete(Release release)
    {
        context.Releases.Remove(release);
    }
}
