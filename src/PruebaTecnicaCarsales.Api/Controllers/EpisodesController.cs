using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCarsales.Application.Interfaces;

namespace PruebaTecnicaCarsales.Api.Controllers;

[ApiController]
[Route("api/episodes")]
public sealed class EpisodesController : ControllerBase
{
    private readonly IRickAndMortyEpisodeService _episodeService;

    public EpisodesController(IRickAndMortyEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    /// <summary>
    /// Obtiene una lista paginada de episodios de Rick and Morty, aplicando filtros opcionales.
    /// </summary>
    /// <param name="page">Número de página a consultar. Debe ser mayor o igual a 1.</param>
    /// <param name="name">Filtro opcional por nombre del episodio.</param>
    /// <param name="episode">Filtro opcional por código del episodio, por ejemplo S01E01.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Resultado HTTP con la lista paginada de episodios o un mensaje de validación.</returns>
    [HttpGet]
    public async Task<IActionResult> GetEpisodes(
        [FromQuery] int page = 1,
        [FromQuery] string? name = null,
        [FromQuery] string? episode = null,
        CancellationToken cancellationToken = default)
    {
        if (page < 1)
        {
            return BadRequest(new
            {
                message = "La página debe ser mayor o igual a 1."
            });
        }

        var result = await _episodeService.GetEpisodesAsync(
            page,
            name,
            episode,
            cancellationToken);

        return Ok(result);
    }
}