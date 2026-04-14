using Recommenda.Domain.Entities;

namespace Recommenda.Application.Services;

/// <summary>Define operações de repositório para avaliações de álbuns.</summary>
public interface IAlbumRatingRepository
{
    IReadOnlyList<AlbumRating> GetByAlbum(Guid albumId);
    IReadOnlyList<AlbumRating> GetByUser(Guid userId);
    AlbumRating Create(AlbumRating rating);
    bool Exists(Guid userId, Guid albumId);
}
