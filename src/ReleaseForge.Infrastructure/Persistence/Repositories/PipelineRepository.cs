using Microsoft.EntityFrameworkCore;
using ReleaseForge.Application.Ports;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Repositories;

internal sealed class PipelineRepository(AppDbContext context) : IPipelineRepository
{
    public async Task<Pipeline?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Pipelines
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Pipeline>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await context.Pipelines
            .Where(p => p.ProjectId == projectId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public void Add(Pipeline pipeline)
    {
        context.Pipelines.Add(pipeline);
    }

    public void Update(Pipeline pipeline)
    {
        context.Pipelines.Update(pipeline);
    }

    public void Delete(Pipeline pipeline)
    {
        context.Pipelines.Remove(pipeline);
    }
}
