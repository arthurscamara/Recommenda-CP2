using Recommenda.Domain.Entities;

namespace Recommenda.Application.DTOs;

public record AlbumRequest(
    string Title,
    DateTime ReleaseDate,
    Guid ArtistId,
    string CoverUrl = ""
)
{
    public Album ToDomain() => new(Title, ReleaseDate, ArtistId, CoverUrl);
}

public record AlbumResponse(Guid Id, string Title, DateTime ReleaseDate, Guid ArtistId)
{
    public static AlbumResponse FromDomain(Album a) => new(a.Id, a.Title, a.ReleaseDate, a.ArtistId);
}
