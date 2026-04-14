using Microsoft.AspNetCore.Mvc;
using Recommenda.Application.DTOs;
using Recommenda.Application.Services;

namespace Recommenda.API.Controllers;

/// <summary>
/// Endpoints para avaliações de álbuns.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AlbumRatingController(IAlbumRatingRepository ratingRepository) : ControllerBase
{
    [HttpGet("album/{albumId:guid}")]
    public IActionResult GetByAlbum(Guid albumId) =>
        Ok(ratingRepository.GetByAlbum(albumId).Select(AlbumRatingResponse.FromDomain));

    [HttpGet("user/{userId:guid}")]
    public IActionResult GetByUser(Guid userId) =>
        Ok(ratingRepository.GetByUser(userId).Select(AlbumRatingResponse.FromDomain));

    [HttpPost]
    public IActionResult Create([FromBody] AlbumRatingRequest request)
    {
        try
        {
            if (ratingRepository.Exists(request.UserId, request.AlbumId))
                return Conflict("Usuário já avaliou este álbum.");

            var rating = ratingRepository.Create(request.ToDomain());
            return Ok(AlbumRatingResponse.FromDomain(rating));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
