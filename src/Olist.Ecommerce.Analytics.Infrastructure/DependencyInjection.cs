using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;
using Olist.Ecommerce.Analytics.Infrastructure.Cloud.Blobs;
using Olist.Ecommerce.Analytics.Infrastructure.Hadoop;

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
            services.AddScoped<IWebHdfsClient, WebHdfsClient>();
            services.AddScoped<IAnalyzerBlobStorage, CloudBlobStorage>(_ =>
                new CloudBlobStorage(configuration.GetConnectionString("AzureStorageAccount"),
                    configuration.GetSection("AnalyzerBlobStorage").GetSection("ContainerName").Value));

            services.AddHttpClient<IWebHdfsClient, WebHdfsClient>();

            return services;
        }
    }
}
