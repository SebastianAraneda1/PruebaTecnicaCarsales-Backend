using System.Net;
using System.Net.Http.Json;
using PruebaTecnicaCarsales.Application.Common.Models;
using PruebaTecnicaCarsales.Application.DTOs.Characters;
using PruebaTecnicaCarsales.Application.Interfaces;
using PruebaTecnicaCarsales.Domain.Entities;
using PruebaTecnicaCarsales.Infrastructure.ExternalServices.RickAndMorty.Responses;

namespace PruebaTecnicaCarsales.Infrastructure.ExternalServices.RickAndMorty;
 
public sealed class RickAndMortyCharacterService : IRickAndMortyCharacterService
{
    private readonly HttpClient _httpClient;
    public RickAndMortyCharacterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene personajes paginados desde la API externa de Rick and Morty, aplicando filtros opcionales.
    /// </summary>
    /// <param name="page">Número de página a consultar.</param>
    /// <param name="name">Filtro opcional por nombre del personaje.</param>
    /// <param name="status">Filtro opcional por estado del personaje.</param>
    /// <param name="species">Filtro opcional por especie del personaje.</param>
    /// <param name="gender">Filtro opcional por género del personaje.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Respuesta paginada con los personajes encontrados.</returns>
    public async Task<PaginatedResponse<CharacterDto>> GetCharactersAsync(
        int page,
        string? name,
        string? status,
        string? species,
        string? gender,
        CancellationToken cancellationToken)
    {
        var queryParams = new List<string>
        {
            $"page={page}"
        };

        if (!string.IsNullOrWhiteSpace(name))
        {
            queryParams.Add($"name={Uri.EscapeDataString(name)}");
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            queryParams.Add($"status={Uri.EscapeDataString(status)}");
        }

        if (!string.IsNullOrWhiteSpace(species))
        {
            queryParams.Add($"species={Uri.EscapeDataString(species)}");
        }

        if (!string.IsNullOrWhiteSpace(gender))
        {
            queryParams.Add($"gender={Uri.EscapeDataString(gender)}");
        }

        var requestUrl = $"character?{string.Join("&", queryParams)}";

        using var response = await _httpClient.GetAsync(requestUrl, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return new PaginatedResponse<CharacterDto>
            {
                Count = 0,
                Pages = 0,
                Results = []
            };
        }

        response.EnsureSuccessStatusCode();

        var data = await response.Content
            .ReadFromJsonAsync<RickAndMortyPaginatedResponse<RickAndMortyCharacterResponse>>(
                cancellationToken);

        if (data is null)
        {
            return new PaginatedResponse<CharacterDto>
            {
                Count = 0,
                Pages = 0,
                CurrentPage = page,
                Results = []
            };
        }

        var characters = data.Results.Select(characterResponse => new Character
        {
            Id = characterResponse.Id,
            Name = characterResponse.Name,
            Status = characterResponse.Status,
            Species = characterResponse.Species,
            Type = characterResponse.Type,
            Gender = characterResponse.Gender,
            Image = characterResponse.Image,
            Episodes = characterResponse.Episodes,
            Url = characterResponse.Url,
            Created = characterResponse.Created
        }).ToList();

        return new PaginatedResponse<CharacterDto>
        {
            Count = data.Info.Count,
            Pages = data.Info.Pages,
            CurrentPage = page,
            Results = characters.Select(character => new CharacterDto
            {
                Id = character.Id,
                Name = character.Name,
                Status = character.Status,
                Species = character.Species,
                Type = character.Type,
                Gender = character.Gender,
                Image = character.Image
            }).ToList()
        };
    }
}