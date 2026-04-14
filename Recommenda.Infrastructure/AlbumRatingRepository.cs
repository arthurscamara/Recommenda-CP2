using Recommenda.Application.Services;
using Recommenda.Domain.Entities;
using Recommenda.Infrastructure.Persistence;

namespace Recommenda.Infrastructure;

public sealed class AlbumRatingRepository(RecommendaContext context) : IAlbumRatingRepository
{
    public IReadOnlyList<AlbumRating> GetByAlbum(Guid albumId) =>
        context.AlbumRatings
            .Where(r => r.AlbumId == albumId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();

    public IReadOnlyList<AlbumRating> GetByUser(Guid userId) =>
        context.AlbumRatings
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();

    public AlbumRating Create(AlbumRating rating)
    {
        context.AlbumRatings.Add(rating);
        context.SaveChanges();
        return rating;
    }

    public bool Exists(Guid userId, Guid albumId) =>
        context.AlbumRatings.Any(r => r.UserId == userId && r.AlbumId == albumId);
}
