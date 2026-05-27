using System.Text.Json.Serialization;

namespace PruebaTecnicaCarsales.Infrastructure.ExternalServices.RickAndMorty.Responses;

public sealed class RickAndMortyPaginatedResponse<T>
{
    [JsonPropertyName("info")]
    public RickAndMortyInfoResponse Info { get; init; } = new();

    [JsonPropertyName("results")]
    public IReadOnlyList<T> Results { get; init; } = [];
}

public sealed class RickAndMortyInfoResponse
{
    [JsonPropertyName("count")]
    public int Count { get; init; }

    [JsonPropertyName("pages")]
    public int Pages { get; init; }

    [JsonPropertyName("next")]
    public string? Next { get; init; }

    [JsonPropertyName("prev")]
    public string? Prev { get; init; }
}