using Microsoft.EntityFrameworkCore;
using Recommenda.Application.Services;
using Recommenda.Domain.Entities;
using Recommenda.Infrastructure.Persistence;

namespace Recommenda.Infrastructure;

public sealed class PlaylistRepository(RecommendaContext context) : IPlaylistRepository
{
    public IReadOnlyList<Playlist> GetByUser(Guid userId) =>
        context.Playlists
            .Include(p => p.Tracks)
            .Where(p => p.UserId == userId)
            .OrderBy(p => p.Name)
            .ToList();

    public Playlist? GetById(Guid id) =>
        context.Playlists
            .Include(p => p.Tracks)
            .FirstOrDefault(p => p.Id == id);

    public Playlist Create(Playlist playlist)
    {
        context.Playlists.Add(playlist);
        context.SaveChanges();
        return playlist;
    }

    public bool Delete(Guid id)
    {
        var playlist = context.Playlists.FirstOrDefault(p => p.Id == id);
        if (playlist is null) return false;
        context.Playlists.Remove(playlist);
        context.SaveChanges();
        return true;
    }
}
