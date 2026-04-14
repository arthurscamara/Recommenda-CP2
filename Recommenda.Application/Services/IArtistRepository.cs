using Recommenda.Domain.Entities;

namespace Recommenda.Application.Services;

/// <summary>Define operações de repositório para artistas.</summary>
public interface IArtistRepository
{
    IReadOnlyList<Artist> GetAll();
    Artist? GetById(Guid id);
    Artist Create(Artist artist);
    bool Delete(Guid id);
}
