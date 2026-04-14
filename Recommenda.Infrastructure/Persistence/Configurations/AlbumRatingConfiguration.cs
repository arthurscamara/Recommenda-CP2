using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommenda.Domain.Entities;

namespace Recommenda.Infrastructure.Persistence.Configurations;

public class AlbumRatingConfiguration : IEntityTypeConfiguration<AlbumRating>
{
    public void Configure(EntityTypeBuilder<AlbumRating> builder)
    {
        builder.ToTable("RC_AlbumRatings");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.UserId).IsRequired();
        builder.Property(r => r.AlbumId).IsRequired();

        builder.Property(r => r.Score).IsRequired();

        builder.Property(r => r.Comment)
            .HasMaxLength(1000)
            .IsRequired(false);

        // Índice composto — um usuário avalia cada álbum apenas uma vez
        builder.HasIndex(r => new { r.UserId, r.AlbumId })
            .IsUnique();
    }
}
