using Recommenda.Domain.Entities;

namespace Recommenda.Application.Services;

/// <summary>Define operações de repositório para faixas.</summary>
public interface ITrackRepository
{
    IReadOnlyList<Track> GetByAlbum(Guid albumId);
    Track? GetById(Guid id);
    Track Create(Track track);
    bool Delete(Guid id);
}
