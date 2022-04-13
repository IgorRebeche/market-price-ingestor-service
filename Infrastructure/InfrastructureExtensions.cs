using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITickerRepository, TickerRepository>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            return services;
        }
    }
}
