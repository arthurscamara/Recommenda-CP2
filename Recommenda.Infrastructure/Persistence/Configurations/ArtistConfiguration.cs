using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommenda.Domain.Entities;

namespace Recommenda.Infrastructure.Persistence.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.ToTable("RC_Artists");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(a => a.Name)
            .IsUnique();

        builder.Property(a => a.Bio)
            .HasMaxLength(2000);

        builder.Property(a => a.Country)
            .HasMaxLength(100)
            .IsRequired();

        // 1:N — Artist → Albums (cascade delete)
        builder.HasMany(a => a.Albums)
            .WithOne(al => al.Artist)
            .HasForeignKey(al => al.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        // N:N — Artist ↔ Genre
        builder.HasMany(a => a.Genres)
            .WithMany(g => g.Artists)
            .UsingEntity(j => j.ToTable("RC_ArtistGenres"));
    }
}
