using Beneficios.Domain.Repositories;
using Beneficios.MongoDB.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

public static class AddRepositorySetup
{
    /// <summary>
    /// Adiciona os repositórios e a configuração do MongoDB ao contêiner de serviços.
    /// </summary>
    /// <param name="services">O contêiner de serviços.</param>
    /// <param name="configuration">As configurações da aplicação.</param>
    /// <returns>O contêiner de serviços atualizado.</returns>
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        // Configura a conexão com o MongoDB
        var mongoConnectionString = configuration.GetConnectionString("MongoDb") ?? "mongodb://localhost:27017";
        var mongoClient = new MongoClient(mongoConnectionString);
        var databaseName = configuration.GetValue<string>("MongoDb:DatabaseName") ?? "BeneficiosDb";
        var database = mongoClient.GetDatabase(databaseName);

        // Registra o banco de dados no contêiner de serviços
        services.AddSingleton(database);

        // Registra os repositórios genéricos como tipos abertos
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Registra o repositório específico para IBeneficioColaboradorRepository
        services.AddScoped<IBeneficioColaboradorRepository, BeneficiosColaraboradorRepository>();

        return services;
    }
}
