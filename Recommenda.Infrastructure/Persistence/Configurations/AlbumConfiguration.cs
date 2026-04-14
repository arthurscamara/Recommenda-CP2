using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommenda.Domain.Entities;

namespace Recommenda.Infrastructure.Persistence.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("RC_Albums");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.ReleaseDate)
            .IsRequired();

        builder.Property(a => a.CoverUrl)
            .HasMaxLength(500);

        builder.Property(a => a.ArtistId)
            .IsRequired();

        // 1:N — Album → Tracks
        builder.HasMany(a => a.Tracks)
            .WithOne(t => t.Album)
            .HasForeignKey(t => t.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N — Album → AlbumRatings
        builder.HasMany(a => a.Ratings)
            .WithOne()
            .HasForeignKey(r => r.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
