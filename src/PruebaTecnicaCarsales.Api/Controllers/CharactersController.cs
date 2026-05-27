using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCarsales.Application.Interfaces;

namespace PruebaTecnicaCarsales.Api.Controllers;

[ApiController]
[Route("api/characters")]
public sealed class CharactersController : ControllerBase
{
    private readonly IRickAndMortyCharacterService _characterService;

    public CharactersController(IRickAndMortyCharacterService characterService)
    {
        _characterService = characterService;
    }

    /// <summary>
    /// Obtiene una lista paginada de personajes de Rick and Morty, aplicando filtros opcionales.
    /// </summary>
    /// <param name="page">Número de página a consultar. Debe ser mayor o igual a 1.</param>
    /// <param name="name">Filtro opcional por nombre del personaje.</param>
    /// <param name="status">Filtro opcional por estado del personaje, por ejemplo Alive, Dead o unknown.</param>
    /// <param name="species">Filtro opcional por especie del personaje.</param>
    /// <param name="gender">Filtro opcional por género del personaje.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Resultado HTTP con la lista paginada de personajes o un mensaje de validación.</returns>
    [HttpGet]
    public async Task<IActionResult> GetCharacters(
        [FromQuery] int page = 1,
        [FromQuery] string? name = null,
        [FromQuery] string? status = null,
        [FromQuery] string? species = null,
        [FromQuery] string? gender = null,
        CancellationToken cancellationToken = default)
    {
        if (page < 1)
        {
            return BadRequest(new
            {
                message = "La página debe ser mayor o igual a 1."
            });
        }

        var result = await _characterService.GetCharactersAsync(
            page,
            name,
            status,
            species,
            gender,
            cancellationToken);

        return Ok(result);
    }
}