using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommenda.Domain.Entities;

namespace Recommenda.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("RC_UserProfiles");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.HasIndex(p => p.UserId)
            .IsUnique();

        builder.Property(p => p.Bio)
            .HasMaxLength(1000);

        builder.Property(p => p.AvatarUrl)
            .HasMaxLength(500);

        builder.Property(p => p.FavoriteGenre)
            .HasMaxLength(100);
    }
}
