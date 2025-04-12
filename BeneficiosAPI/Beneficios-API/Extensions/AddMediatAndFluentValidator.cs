using Beneficios.Application.Handlers;
using FluentValidation;

namespace Beneficios_API.Extensions;

/// <summary>
/// Provides extension methods for configuring services in the application.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers MediatR handlers and FluentValidation validators in the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    internal static IServiceCollection AddMediatAndFluentValidator(this IServiceCollection services)
    {
        // Get the assembly containing the IncluirBeneficioColaboradorHandler
        var assembly = typeof(IncluirBeneficioColaboradorHandler).Assembly;

        // Scan the assembly for FluentValidation validators and register them
        AssemblyScanner
            .FindValidatorsInAssemblies(new[] { assembly })
            .ForEach(result =>
            {
                var type = result.InterfaceType; // The interface type of the validator
                var validator = result.ValidatorType; // The concrete validator type
                if (type != null && validator != null)
                {
                    // Register the validator as a scoped service
                    services.AddScoped(type, validator);
                }
            });

        return services;
    }
}
