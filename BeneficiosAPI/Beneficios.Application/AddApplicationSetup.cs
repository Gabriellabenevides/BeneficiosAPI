using Beneficios.MongoDB.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Beneficios.Application;

/// <summary>
/// Classe estática responsável por configurar os serviços da camada de aplicação.
/// </summary>
public static class AddApplicationSetup
{
    /// <summary>
    /// Adiciona os serviços necessários para a camada de aplicação ao contêiner de injeção de dependência.
    /// </summary>
    /// <param name="services">O contêiner de serviços <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">A configuração da aplicação <see cref="IConfiguration"/>.</param>
    /// <returns>O contêiner de serviços atualizado <see cref="IServiceCollection"/>.</returns>
    /// <remarks>
    /// Este método utiliza o método de extensão <c>AddRespository</c> para registrar os repositórios necessários.
    /// Certifique-se de que o namespace correto para <c>AddRespository</c> está incluído.
    /// </remarks>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository(configuration);

        return services;
    }
}
