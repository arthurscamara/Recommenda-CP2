using Recommenda.Domain.Entities;

namespace Recommenda.Application.Services;

/// <summary>Define operações de repositório para playlists.</summary>
public interface IPlaylistRepository
{
    IReadOnlyList<Playlist> GetByUser(Guid userId);
    Playlist? GetById(Guid id);
    Playlist Create(Playlist playlist);
    bool Delete(Guid id);
}
