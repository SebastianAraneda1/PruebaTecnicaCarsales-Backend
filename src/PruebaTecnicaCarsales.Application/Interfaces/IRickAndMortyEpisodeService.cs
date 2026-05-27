using PruebaTecnicaCarsales.Application.Common.Models;
using PruebaTecnicaCarsales.Application.DTOs.Episodes;

namespace PruebaTecnicaCarsales.Application.Interfaces;

public interface IRickAndMortyEpisodeService
{
    Task<PaginatedResponse<EpisodeDto>> GetEpisodesAsync(
        int page,
        string? name,
        string? episode,
        CancellationToken cancellationToken);
}