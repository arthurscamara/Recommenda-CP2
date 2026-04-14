using Recommenda.Domain.Entities;

namespace Recommenda.Application.DTOs;

public record AlbumRatingRequest(Guid UserId, Guid AlbumId, int Score, string? Comment = null)
{
    public AlbumRating ToDomain() => new(UserId, AlbumId, Score, Comment);
}

public record AlbumRatingResponse(Guid Id, Guid UserId, Guid AlbumId, int Score, string? Comment)
{
    public static AlbumRatingResponse FromDomain(AlbumRating r) =>
        new(r.Id, r.UserId, r.AlbumId, r.Score, r.Comment);
}
