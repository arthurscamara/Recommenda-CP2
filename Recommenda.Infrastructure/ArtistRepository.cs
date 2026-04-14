using Microsoft.EntityFrameworkCore;
using Recommenda.Application.Services;
using Recommenda.Domain.Entities;
using Recommenda.Infrastructure.Persistence;

namespace Recommenda.Infrastructure;

public sealed class ArtistRepository(RecommendaContext context) : IArtistRepository
{
    public IReadOnlyList<Artist> GetAll() =>
        context.Artists.Include(a => a.Genres).OrderBy(a => a.Name).ToList();

    public Artist? GetById(Guid id) =>
        context.Artists.Include(a => a.Genres).FirstOrDefault(a => a.Id == id);

    public Artist Create(Artist artist)
    {
        context.Artists.Add(artist);
        context.SaveChanges();
        return artist;
    }

    public bool Delete(Guid id)
    {
        var artist = context.Artists.FirstOrDefault(a => a.Id == id);
        if (artist is null) return false;
        context.Artists.Remove(artist);
        context.SaveChanges();
        return true;
    }
}
