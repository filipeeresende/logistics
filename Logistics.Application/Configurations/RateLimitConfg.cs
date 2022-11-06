using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Logistics.Application.Configurations
{
    public static class RateLimitConfg
    {
        public static IServiceCollection AddRateLimit(this IServiceCollection services)
        {

            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-IP";
                options.ClientIdHeader = "X-ClientId";
                options.GeneralRules = new List<RateLimitRule>

        {

            new RateLimitRule
            {
                Endpoint = $"*",
                Period = "1m",
                Limit = 20,
            },

        };
            });

            services.AddInMemoryRateLimiting();

            return services;
        }
    }
}
