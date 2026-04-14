using Microsoft.EntityFrameworkCore;
using Recommenda.Domain.Entities;
using Recommenda.Infrastructure.Persistence.Configurations;

namespace Recommenda.Infrastructure.Persistence;

/// <summary>
/// DbContext principal do Recommenda.
/// Todas as entidades do domínio musical estão registradas aqui.
/// </summary>
public class RecommendaContext(DbContextOptions<RecommendaContext> options) : DbContext(options)
{
    public DbSet<Artist>       Artists       { get; set; }
    public DbSet<Album>        Albums        { get; set; }
    public DbSet<Track>        Tracks        { get; set; }
    public DbSet<Genre>        Genres        { get; set; }
    public DbSet<User>         Users         { get; set; }
    public DbSet<UserProfile>  UserProfiles  { get; set; }
    public DbSet<AlbumRating>  AlbumRatings  { get; set; }
    public DbSet<TrackRating>  TrackRatings  { get; set; }
    public DbSet<Playlist>     Playlists     { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecommendaContext).Assembly);
    }
}
