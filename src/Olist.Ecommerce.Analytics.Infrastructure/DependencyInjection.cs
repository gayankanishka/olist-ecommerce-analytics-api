using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;
using Olist.Ecommerce.Analytics.Infrastructure.Hadoop;
using Olist.Ecommerce.Analytics.Infrastructure.Persistence;

namespace Olist.Ecommerce.Analytics.Infrastructure
{
    /// <summary>
    /// Dependency injection extension to configure Infrastructure layer services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configure Infrastructure layer services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(_ =>
            {
                _.UseMySQL
                (
                    configuration.GetConnectionString("OlistDb"),
                    h => h.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                );
            });

            services.AddScoped<IApplicationDbContext>(_ =>
                _.GetService<ApplicationDbContext>());
            services.AddScoped<IWebHdfsClient, WebHdfsClient>();

            services.AddHttpClient<IWebHdfsClient, WebHdfsClient>();

            return services;
        }
    }
}
