using Recommenda.Domain.Entities;

namespace Recommenda.Application.Services;

/// <summary>Define operações de repositório para gêneros musicais.</summary>
public interface IGenreRepository
{
    IReadOnlyList<Genre> GetAll();
    Genre? GetById(Guid id);
    Genre Create(Genre genre);
    bool Delete(Guid id);
}
