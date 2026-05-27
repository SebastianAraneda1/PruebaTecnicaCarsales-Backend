namespace PruebaTecnicaCarsales.Application.Common.Models;

public sealed class PaginatedResponse<T>
{
    public int Count { get; init; }
    public int Pages { get; init; }
    public int CurrentPage { get; init; }
    public bool HasNextPage => CurrentPage < Pages;
    public bool HasPreviousPage => CurrentPage > 1;
    public IReadOnlyList<T> Results { get; init; } = [];
}