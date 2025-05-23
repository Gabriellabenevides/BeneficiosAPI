using Beneficios_API.Extensions.SwaggerConfigurations;
using Beneficios.MongoDB.Repositories;

namespace Beneficios_API;

/// <summary>
/// Classe principal responsável por iniciar e configurar a aplicação Beneficios API.
/// </summary>
public static class Program
{
    /// <summary>
    /// Método principal responsável por iniciar a aplicação Beneficios API.
    /// </summary>
    /// <param name="args">Argumentos de linha de comando.</param>
    /// <returns>Uma tarefa que representa a execução assíncrona do programa.</returns>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração de serviços
        builder.Services
            .AddSwaggerConfig(builder.Configuration)
            .AddControllers();

        // Adiciona a configuração do MongoDB e repositórios
        builder.Services.AddRespository(builder.Configuration);

        var app = builder.Build();

        app.UsePathBase("/beneficios-api");

        app.UseRouting();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/beneficios-api/swagger/v1/swagger.json", "Beneficios API V1");
            options.RoutePrefix = string.Empty;
        });

        app.MapControllers();

        await app.RunAsync();
    }
}
