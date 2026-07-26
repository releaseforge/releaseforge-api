using Microsoft.EntityFrameworkCore;
using ReleaseForge.Application.Ports;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Repositories;

internal sealed class ProjectRepository(AppDbContext context) : IProjectRepository
{
    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Projects
            .Include(p => p.Releases)
            .Include(p => p.Environments)
            .Include(p => p.Pipelines)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Project?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await context.Projects
            .Include(p => p.Releases)
            .Include(p => p.Environments)
            .Include(p => p.Pipelines)
            .FirstOrDefaultAsync(p => p.Slug == slug, cancellationToken);
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Projects
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public void Add(Project project)
    {
        context.Projects.Add(project);
    }

    public void Update(Project project)
    {
        context.Projects.Update(project);
    }

    public void Delete(Project project)
    {
        context.Projects.Remove(project);
    }
}
