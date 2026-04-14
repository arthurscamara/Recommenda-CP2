using Microsoft.AspNetCore.Mvc;
using Recommenda.Application.DTOs;
using Recommenda.Application.Services;

namespace Recommenda.API.Controllers;

/// <summary>
/// Endpoints para gerenciamento de álbuns.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AlbumController(IAlbumRepository albumRepository) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok(albumRepository.GetAll().Select(AlbumResponse.FromDomain));

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var album = albumRepository.GetById(id);
        return album is null ? NotFound() : Ok(AlbumResponse.FromDomain(album));
    }

    [HttpGet("artist/{artistId:guid}")]
    public IActionResult GetByArtist(Guid artistId) =>
        Ok(albumRepository.GetByArtist(artistId).Select(AlbumResponse.FromDomain));

    [HttpPost]
    public IActionResult Create([FromBody] AlbumRequest request)
    {
        try
        {
            var album = albumRepository.Create(request.ToDomain());
            return Ok(AlbumResponse.FromDomain(album));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) =>
        albumRepository.Delete(id) ? NoContent() : NotFound();
}
