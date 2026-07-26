using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReleaseForge.Domain.Entities;
using ReleaseForge.Infrastructure.Identity;
using ReleaseForge.Infrastructure.Persistence.Configurations;

namespace ReleaseForge.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Release> Releases => Set<Release>();
    public DbSet<Deployment> Deployments => Set<Deployment>();
    public DbSet<DeploymentEnvironment> Environments => Set<DeploymentEnvironment>();
    public DbSet<Pipeline> Pipelines => Set<Pipeline>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new ReleaseConfiguration());
        modelBuilder.ApplyConfiguration(new DeploymentConfiguration());
        modelBuilder.ApplyConfiguration(new DeploymentEnvironmentConfiguration());
        modelBuilder.ApplyConfiguration(new PipelineConfiguration());

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.DisplayName).HasMaxLength(100);
        });
    }
}
