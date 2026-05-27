using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnicaCarsales.Application.Interfaces;
using PruebaTecnicaCarsales.Infrastructure.ExternalServices.RickAndMorty;

namespace PruebaTecnicaCarsales.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var rickAndMortyApiOptions = configuration
            .GetSection("RickAndMortyApi")
            .Get<RickAndMortyApiOptions>();

        if (rickAndMortyApiOptions is null ||
            string.IsNullOrWhiteSpace(rickAndMortyApiOptions.BaseUrl))
        {
            throw new InvalidOperationException("RickAndMortyApi:BaseUrl configuration is required.");
        }

        services.AddHttpClient<IRickAndMortyEpisodeService, RickAndMortyEpisodeService>(client =>
        {
            client.BaseAddress = new Uri(rickAndMortyApiOptions.BaseUrl);
        });
        services.AddHttpClient<IRickAndMortyCharacterService, RickAndMortyCharacterService>(client =>
        {
            client.BaseAddress = new Uri(rickAndMortyApiOptions.BaseUrl);
        });
        return services;
    }
}