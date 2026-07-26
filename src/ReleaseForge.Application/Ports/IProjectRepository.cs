using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Application.Ports;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Project?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Project project);
    void Update(Project project);
    void Delete(Project project);
}
