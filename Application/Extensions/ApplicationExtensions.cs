using Application.UseCases;
using Application.UseCases.GetTicker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IInsertTickerUseCase, InsertTickerUseCase>();
            services.AddScoped<IGetTickerUseCase, GetTickerUseCase>();
            return services;
        }
    }
}
