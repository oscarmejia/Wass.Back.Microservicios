using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wass.Back.Empresa.Rabbit.Context;

namespace Wass.Back.Empresa.Configuraciones
{
    public static class ConfigureConnections
    {
        /// <summary>
        /// Adds the connection provider.
        /// </summary>
        /// <returns>The connection provider.</returns>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool <EmpresaContext>(options => options.UseSqlServer(configuration["SecretsKeyApp:DbEmpresa"],
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 7,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
                }));

            return services;
        }
        
    }
}
