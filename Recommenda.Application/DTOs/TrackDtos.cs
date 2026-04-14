using Recommenda.Domain.Entities;

namespace Recommenda.Application.DTOs;

public record TrackRequest(string Title, int DurationSeconds, int TrackNumber, Guid AlbumId)
{
    public Track ToDomain() => new(Title, DurationSeconds, TrackNumber, AlbumId);
}

public record TrackResponse(Guid Id, string Title, string Duration, int TrackNumber, Guid AlbumId)
{
    public static TrackResponse FromDomain(Track t) =>
        new(t.Id, t.Title, t.FormattedDuration, t.TrackNumber, t.AlbumId);
}
