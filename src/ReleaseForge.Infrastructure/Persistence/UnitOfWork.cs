using ReleaseForge.Application.Ports;

namespace ReleaseForge.Infrastructure.Persistence;

internal sealed class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
