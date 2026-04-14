using Recommenda.Application.Services;
using Recommenda.Domain.Entities;
using Recommenda.Infrastructure.Persistence;

namespace Recommenda.Infrastructure;

public sealed class GenreRepository(RecommendaContext context) : IGenreRepository
{
    public IReadOnlyList<Genre> GetAll() =>
        context.Genres.OrderBy(g => g.Name).ToList();

    public Genre? GetById(Guid id) =>
        context.Genres.FirstOrDefault(g => g.Id == id);

    public Genre Create(Genre genre)
    {
        context.Genres.Add(genre);
        context.SaveChanges();
        return genre;
    }

    public bool Delete(Guid id)
    {
        var genre = context.Genres.FirstOrDefault(g => g.Id == id);
        if (genre is null) return false;
        context.Genres.Remove(genre);
        context.SaveChanges();
        return true;
    }
}
