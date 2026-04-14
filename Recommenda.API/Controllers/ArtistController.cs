using Microsoft.AspNetCore.Mvc;
using Recommenda.Application.DTOs;
using Recommenda.Application.Services;

namespace Recommenda.API.Controllers;

/// <summary>
/// Endpoints para gerenciamento de artistas.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ArtistController(IArtistRepository artistRepository) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok(artistRepository.GetAll().Select(ArtistResponse.FromDomain));

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var artist = artistRepository.GetById(id);
        return artist is null ? NotFound() : Ok(ArtistResponse.FromDomain(artist));
    }

    [HttpPost]
    public IActionResult Create([FromBody] ArtistRequest request)
    {
        try
        {
            var artist = artistRepository.Create(request.ToDomain());
            return Ok(ArtistResponse.FromDomain(artist));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) =>
        artistRepository.Delete(id) ? NoContent() : NotFound();
}
