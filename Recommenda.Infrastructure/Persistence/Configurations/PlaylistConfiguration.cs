using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommenda.Domain.Entities;

namespace Recommenda.Infrastructure.Persistence.Configurations;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("RC_Playlists");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.IsPublic)
            .HasDefaultValue(true);

        builder.Property(p => p.UserId)
            .IsRequired();

        // N:N — Playlist ↔ Track
        builder.HasMany(p => p.Tracks)
            .WithMany()
            .UsingEntity(j => j.ToTable("RC_PlaylistTracks"));
    }
}
