using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Configurations;

internal sealed class DeploymentConfiguration : IEntityTypeConfiguration<Deployment>
{
    public void Configure(EntityTypeBuilder<Deployment> builder)
    {
        builder.ToTable("deployments");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedNever();

        builder.Property(d => d.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
    }
}
