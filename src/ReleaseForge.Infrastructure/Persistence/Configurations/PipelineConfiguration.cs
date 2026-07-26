using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Configurations;

internal sealed class PipelineConfiguration : IEntityTypeConfiguration<Pipeline>
{
    public void Configure(EntityTypeBuilder<Pipeline> builder)
    {
        builder.ToTable("pipelines");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
