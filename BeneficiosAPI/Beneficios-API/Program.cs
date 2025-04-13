using Beneficios.Application;
using Beneficios.Application.Handlers;
using Beneficios_API.Extensions.SwaggerConfigurations;

namespace Beneficios_API;

/// <summary>
/// Classe principal respons�vel por iniciar e configurar a aplica��o Beneficios API.
/// </summary>
public static class Program
{
    /// <summary>
    /// M�todo principal respons�vel por iniciar a aplica��o Beneficios API.
    /// </summary>
    /// <param name="args">Argumentos de linha de comando.</param>
    /// <returns>Uma tarefa que representa a execu��o ass�ncrona do programa.</returns>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configura��o de servi�os
        builder.Services
            .AddSwaggerConfig(builder.Configuration) 
            .AddApplication(builder.Configuration)   
            .AddControllers();

        // Adiciona a configura��o do MongoDB e reposit�rios
        builder.Services.AddRepository(builder.Configuration);

        // With this line:
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IncluirBeneficioColaboradorHandler).Assembly));

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
