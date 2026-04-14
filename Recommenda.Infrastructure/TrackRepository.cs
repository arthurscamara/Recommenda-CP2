using Microsoft.EntityFrameworkCore;
using Recommenda.Application.Services;
using Recommenda.Domain.Entities;
using Recommenda.Infrastructure.Persistence;

namespace Recommenda.Infrastructure;

public sealed class TrackRepository(RecommendaContext context) : ITrackRepository
{
    public IReadOnlyList<Track> GetByAlbum(Guid albumId) =>
        context.Tracks.Where(t => t.AlbumId == albumId).OrderBy(t => t.TrackNumber).ToList();

    public Track? GetById(Guid id) =>
        context.Tracks.Include(t => t.Genres).FirstOrDefault(t => t.Id == id);

    public Track Create(Track track)
    {
        context.Tracks.Add(track);
        context.SaveChanges();
        return track;
    }

    public bool Delete(Guid id)
    {
        var track = context.Tracks.FirstOrDefault(t => t.Id == id);
        if (track is null) return false;
        context.Tracks.Remove(track);
        context.SaveChanges();
        return true;
    }
}
