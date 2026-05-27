namespace PruebaTecnicaCarsales.Application.DTOs.Characters;

public sealed class CharacterDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public string Species { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    public string Image { get; init; } = string.Empty;
}