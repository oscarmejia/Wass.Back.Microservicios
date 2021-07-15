using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wass.Back.Programador.Rabbit.Context;

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
            services.AddDbContextPool<ProgramadorContext>(options => options.UseSqlServer(configuration["SecretsKeyApp:DbProgramador"],
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
