using System.Net;
using System.Text.Json;

namespace PruebaTecnicaCarsales.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Ejecuta el middleware de manejo global de errores para interceptar excepciones no controladas durante la petición HTTP.
    /// </summary>
    /// <param name="context">Contexto HTTP de la petición actual.</param>
    /// <returns>Una tarea que representa la ejecución asincrónica del middleware.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "An error occurred while requesting the external API.");

            context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = "No fue posible obtener información desde el servicio externo."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = "Ocurrió un error inesperado."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}