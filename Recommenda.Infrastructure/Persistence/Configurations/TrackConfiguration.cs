using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommenda.Domain.Entities;

namespace Recommenda.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.ToTable("RC_Tracks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.DurationSeconds)
            .IsRequired();

        builder.Property(t => t.TrackNumber)
            .IsRequired();

        builder.Property(t => t.AlbumId)
            .IsRequired();

        // Índice composto — garante ordem única dentro do álbum
        builder.HasIndex(t => new { t.AlbumId, t.TrackNumber })
            .IsUnique();

        // N:N — Track ↔ Genre
        builder.HasMany(t => t.Genres)
            .WithMany(g => g.Tracks)
            .UsingEntity(j => j.ToTable("RC_TrackGenres"));

        // 1:N — Track → TrackRatings
        builder.HasMany(t => t.Ratings)
            .WithOne()
            .HasForeignKey(r => r.TrackId)
            .OnDelete(DeleteBehavior.Cascade);

        // FormattedDuration é propriedade calculada — ignorar no banco
        builder.Ignore(t => t.FormattedDuration);
    }
}
