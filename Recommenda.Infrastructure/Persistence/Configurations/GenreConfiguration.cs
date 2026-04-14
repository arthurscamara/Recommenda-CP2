using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommenda.Domain.Entities;

namespace Recommenda.Infrastructure.Persistence.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("RC_Genres");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(g => g.Name)
            .IsUnique();

        builder.Property(g => g.Description)
            .HasMaxLength(500);

        // N:N com Track configurado em TrackConfiguration
    }
}
