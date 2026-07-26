using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Slug)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.HasIndex(p => p.Slug)
            .IsUnique();

        builder.HasMany(p => p.Releases)
            .WithOne()
            .HasForeignKey(r => r.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Environments)
            .WithOne()
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Pipelines)
            .WithOne()
            .HasForeignKey(p => p.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
