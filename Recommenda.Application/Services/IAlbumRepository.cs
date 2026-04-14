using Recommenda.Domain.Entities;

namespace Recommenda.Application.Services;

/// <summary>Define operações de repositório para álbuns.</summary>
public interface IAlbumRepository
{
    IReadOnlyList<Album> GetAll();
    Album? GetById(Guid id);
    IReadOnlyList<Album> GetByArtist(Guid artistId);
    Album Create(Album album);
    bool Delete(Guid id);
}
