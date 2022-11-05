using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Application.Configurations
{
    public static class ContextConfig
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LogisticsContext>(options => options
           .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
