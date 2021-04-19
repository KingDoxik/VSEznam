using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Storage
{
    /// <summary>
    /// Index storage extensions
    /// </summary>
    public static class IndexStorageExtensions
    {
        /// <summary>
        /// Helper function that registers all dependecies for Elastic Search Index Storage to dependency injection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddElasticSearchIndexStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ElasticSearchConfiguration>(configuration.GetSection("ElasticSearch"));
            services.AddTransient<IIndexStorage, ElasticSearchIndexStorage>();
            return services;
        }
    }
}
