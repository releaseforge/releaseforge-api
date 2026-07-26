using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ReleaseForge.Infrastructure.Persistence;

namespace ReleaseForge.Api;

internal sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=releaseforge;Username=postgres;Password=admin123");

        return new AppDbContext(optionsBuilder.Options);
    }
}
