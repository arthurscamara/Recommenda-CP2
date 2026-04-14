using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommenda.Domain.Entities;

namespace Recommenda.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("RC_Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Password)
            .HasMaxLength(500)
            .IsRequired();

        // Propriedades privadas mapeadas pelo nome do backing field
        builder.Property<string>("Salt")
            .HasColumnName("Salt")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property<DateOnly>("BirthDate")
            .HasColumnName("BirthDate")
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v));

        // 1:1 — User → UserProfile (opcional)
        builder.HasOne(u => u.Profile)
            .WithOne()
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N — User → AlbumRatings
        builder.HasMany(u => u.AlbumRatings)
            .WithOne()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N — User → TrackRatings
        builder.HasMany(u => u.TrackRatings)
            .WithOne()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N — User → Playlists
        builder.HasMany(u => u.Playlists)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
