using System.Net;
using System.Net.Http.Json;
using PruebaTecnicaCarsales.Application.Common.Models;
using PruebaTecnicaCarsales.Application.DTOs.Episodes;
using PruebaTecnicaCarsales.Application.Interfaces;
using PruebaTecnicaCarsales.Infrastructure.ExternalServices.RickAndMorty.Responses;
using PruebaTecnicaCarsales.Domain.Entities;

namespace PruebaTecnicaCarsales.Infrastructure.ExternalServices.RickAndMorty;

public sealed class RickAndMortyEpisodeService : IRickAndMortyEpisodeService
{
    private readonly HttpClient _httpClient;

    public RickAndMortyEpisodeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene episodios paginados desde la API externa de Rick and Morty, aplicando filtros opcionales.
    /// </summary>
    /// <param name="page">Número de página a consultar.</param>
    /// <param name="name">Filtro opcional por nombre del episodio.</param>
    /// <param name="episode">Filtro opcional por código del episodio.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Respuesta paginada con los episodios encontrados.</returns>
    public async Task<PaginatedResponse<EpisodeDto>> GetEpisodesAsync(
        int page,
        string? name,
        string? episode,
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

        if (!string.IsNullOrWhiteSpace(episode))
        {
            queryParams.Add($"episode={Uri.EscapeDataString(episode)}");
        }

        var requestUrl = $"episode?{string.Join("&", queryParams)}";

        using var response = await _httpClient.GetAsync(requestUrl, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return new PaginatedResponse<EpisodeDto>
            {
                Count = 0,
                Pages = 0,
                Results = []
            };
        }

        response.EnsureSuccessStatusCode();

        var data = await response.Content
            .ReadFromJsonAsync<RickAndMortyPaginatedResponse<RickAndMortyEpisodeResponse>>(
                cancellationToken);

        if (data is null)
        {
            return new PaginatedResponse<EpisodeDto>();
        }

        var episodes = data.Results.Select(episodeResponse => new Episode
        {
            Id = episodeResponse.Id,
            Name = episodeResponse.Name,
            AirDate = episodeResponse.AirDate,
            Code = episodeResponse.Episode,
            Characters = episodeResponse.Characters,
            Url = episodeResponse.Url,
            Created = episodeResponse.Created
        }).ToList();

        return new PaginatedResponse<EpisodeDto>
        {
            Count = data.Info.Count,
            Pages = data.Info.Pages,
            CurrentPage = page,
            Results = episodes.Select(domainEpisode => new EpisodeDto
            {
                Id = domainEpisode.Id,
                Name = domainEpisode.Name,
                AirDate = domainEpisode.AirDate,
                Episode = domainEpisode.Code
            }).ToList()
        };
    }
}