using AspNetCoreRateLimit;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Domain.Services;
using Logistics.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Application.Configurations
{
    public static class DependecyConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            //Repositories
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();


            //Services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOccurrenceService, OccurrenceService>();

            return services;
        }
    }
}
