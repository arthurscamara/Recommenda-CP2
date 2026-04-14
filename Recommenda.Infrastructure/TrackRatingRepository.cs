using Recommenda.Application.Services;
using Recommenda.Domain.Entities;
using Recommenda.Infrastructure.Persistence;

namespace Recommenda.Infrastructure;

public sealed class TrackRatingRepository(RecommendaContext context) : ITrackRatingRepository
{
    public IReadOnlyList<TrackRating> GetByTrack(Guid trackId) =>
        context.TrackRatings
            .Where(r => r.TrackId == trackId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();

    public IReadOnlyList<TrackRating> GetByUser(Guid userId) =>
        context.TrackRatings
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();

    public TrackRating Create(TrackRating rating)
    {
        context.TrackRatings.Add(rating);
        context.SaveChanges();
        return rating;
    }

    public bool Exists(Guid userId, Guid trackId) =>
        context.TrackRatings.Any(r => r.UserId == userId && r.TrackId == trackId);
}
