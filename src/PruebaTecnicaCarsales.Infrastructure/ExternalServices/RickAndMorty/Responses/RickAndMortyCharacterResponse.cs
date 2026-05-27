using System.Text.Json.Serialization;

namespace PruebaTecnicaCarsales.Infrastructure.ExternalServices.RickAndMorty.Responses;

public sealed class RickAndMortyCharacterResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("species")]
    public string Species { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("gender")]
    public string Gender { get; init; } = string.Empty;

    [JsonPropertyName("image")]
    public string Image { get; init; } = string.Empty;

    [JsonPropertyName("episode")]
    public IReadOnlyList<string> Episodes { get; init; } = [];

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    [JsonPropertyName("created")]
    public DateTime Created { get; init; }
}