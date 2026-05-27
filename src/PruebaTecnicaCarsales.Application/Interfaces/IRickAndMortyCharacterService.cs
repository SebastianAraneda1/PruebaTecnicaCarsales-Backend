using PruebaTecnicaCarsales.Application.Common.Models;
using PruebaTecnicaCarsales.Application.DTOs.Characters;

namespace PruebaTecnicaCarsales.Application.Interfaces;

public interface IRickAndMortyCharacterService
{
    Task<PaginatedResponse<CharacterDto>> GetCharactersAsync(
        int page,
        string? name,
        string? status,
        string? species,
        string? gender,
        CancellationToken cancellationToken);
}