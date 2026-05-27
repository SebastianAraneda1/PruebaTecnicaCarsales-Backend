using System.Text.Json.Serialization;

namespace PruebaTecnicaCarsales.Infrastructure.ExternalServices.RickAndMorty.Responses;

public sealed class RickAndMortyEpisodeResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("air_date")]
    public string AirDate { get; init; } = string.Empty;

    [JsonPropertyName("episode")]
    public string Episode { get; init; } = string.Empty;

    [JsonPropertyName("characters")]
    public IReadOnlyList<string> Characters { get; init; } = [];

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    [JsonPropertyName("created")]
    public DateTime Created { get; init; }
}