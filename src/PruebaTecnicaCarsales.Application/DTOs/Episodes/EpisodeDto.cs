namespace PruebaTecnicaCarsales.Application.DTOs.Episodes;

public sealed class EpisodeDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string AirDate { get; init; } = string.Empty;
    public string Episode { get; init; } = string.Empty;
}