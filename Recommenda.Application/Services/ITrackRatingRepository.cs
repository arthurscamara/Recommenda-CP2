using Recommenda.Domain.Entities;

namespace Recommenda.Application.Services;

/// <summary>Define operações de repositório para avaliações de faixas.</summary>
public interface ITrackRatingRepository
{
    IReadOnlyList<TrackRating> GetByTrack(Guid trackId);
    IReadOnlyList<TrackRating> GetByUser(Guid userId);
    TrackRating Create(TrackRating rating);
    bool Exists(Guid userId, Guid trackId);
}
