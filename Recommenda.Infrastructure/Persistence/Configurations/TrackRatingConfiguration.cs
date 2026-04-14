using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommenda.Domain.Entities;

namespace Recommenda.Infrastructure.Persistence.Configurations;

public class TrackRatingConfiguration : IEntityTypeConfiguration<TrackRating>
{
    public void Configure(EntityTypeBuilder<TrackRating> builder)
    {
        builder.ToTable("RC_TrackRatings");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.UserId).IsRequired();
        builder.Property(r => r.TrackId).IsRequired();

        builder.Property(r => r.Score).IsRequired();

        builder.Property(r => r.Comment)
            .HasMaxLength(1000)
            .IsRequired(false);

        // Índice composto — um usuário avalia cada faixa apenas uma vez
        builder.HasIndex(r => new { r.UserId, r.TrackId })
            .IsUnique();
    }
}
