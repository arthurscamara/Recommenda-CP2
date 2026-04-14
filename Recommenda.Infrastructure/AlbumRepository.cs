using Microsoft.EntityFrameworkCore;
using Recommenda.Application.Services;
using Recommenda.Domain.Entities;
using Recommenda.Infrastructure.Persistence;

namespace Recommenda.Infrastructure;

public sealed class AlbumRepository(RecommendaContext context) : IAlbumRepository
{
    public IReadOnlyList<Album> GetAll() =>
        context.Albums.Include(a => a.Artist).OrderBy(a => a.Title).ToList();

    public Album? GetById(Guid id) =>
        context.Albums.Include(a => a.Artist).Include(a => a.Tracks).FirstOrDefault(a => a.Id == id);

    public IReadOnlyList<Album> GetByArtist(Guid artistId) =>
        context.Albums.Where(a => a.ArtistId == artistId).OrderBy(a => a.ReleaseDate).ToList();

    public Album Create(Album album)
    {
        context.Albums.Add(album);
        context.SaveChanges();
        return album;
    }

    public bool Delete(Guid id)
    {
        var album = context.Albums.FirstOrDefault(a => a.Id == id);
        if (album is null) return false;
        context.Albums.Remove(album);
        context.SaveChanges();
        return true;
    }
}
