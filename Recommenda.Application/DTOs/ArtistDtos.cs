using Recommenda.Domain.Entities;

namespace Recommenda.Application.DTOs;

public record ArtistRequest(string Name, string Bio, string Country)
{
    public Artist ToDomain() => new(Name, Bio, Country);
}

public record ArtistResponse(Guid Id, string Name, string Country)
{
    public static ArtistResponse FromDomain(Artist a) => new(a.Id, a.Name, a.Country);
}
