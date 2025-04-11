using Beneficios.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Beneficios.MongoDB.Repositories;

/// <summary>
/// Classe responsável por configurar a injeção de dependência para os repositórios.
/// </summary>
public static class AddRepositorySetup
{
    /// <summary>
    /// Adiciona os repositórios e a configuração do MongoDB ao contêiner de serviços.
    /// </summary>
    /// <param name="services">O contêiner de serviços.</param>
    /// <param name="configuration">As configurações da aplicação.</param>
    /// <returns>O contêiner de serviços atualizado.</returns>
    public static IServiceCollection AddRespository(this IServiceCollection services, IConfiguration configuration)
    {
        // Configura a conexão com o MongoDB
        var mongoConnectionString = configuration.GetConnectionString("MongoDb") ?? "mongodb://localhost:27017";
        var mongoClient = new MongoClient(mongoConnectionString);
        var databaseName = configuration.GetValue<string>("MongoDb:DatabaseName") ?? "BeneficiosDb";
        var database = mongoClient.GetDatabase(databaseName);

        // Registra o banco de dados no contêiner de serviços
        services.AddSingleton(database);

        // Registra os repositórios genéricos
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}

