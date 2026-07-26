using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReleaseForge.Domain.Entities;

namespace ReleaseForge.Infrastructure.Persistence.Configurations;

internal sealed class ReleaseConfiguration : IEntityTypeConfiguration<Release>
{
    public void Configure(EntityTypeBuilder<Release> builder)
    {
        builder.ToTable("releases");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedNever();

        builder.Property(r => r.Version)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(r => r.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(r => r.Description)
            .HasMaxLength(1000);

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(r => r.Deployments)
            .WithOne()
            .HasForeignKey(d => d.ReleaseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
