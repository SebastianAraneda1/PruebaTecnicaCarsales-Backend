namespace PruebaTecnicaCarsales.Domain.Entities;

public sealed class Character
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public string Species { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    public string Image { get; init; } = string.Empty;
    public IReadOnlyList<string> Episodes { get; init; } = [];
    public string Url { get; init; } = string.Empty;
    public DateTime Created { get; init; }
}