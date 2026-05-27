namespace PruebaTecnicaCarsales.Domain.Entities;

public sealed class Episode
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string AirDate { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public IReadOnlyList<string> Characters { get; init; } = [];
    public string Url { get; init; } = string.Empty;
    public DateTime Created { get; init; }
}