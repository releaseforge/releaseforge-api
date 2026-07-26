using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Configurations;

internal sealed class DeploymentEnvironmentConfiguration : IEntityTypeConfiguration<DeploymentEnvironment>
{
    public void Configure(EntityTypeBuilder<DeploymentEnvironment> builder)
    {
        builder.ToTable("environments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Slug)
            .HasMaxLength(100)
            .IsRequired();
    }
}
