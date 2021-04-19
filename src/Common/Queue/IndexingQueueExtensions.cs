using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Queue
{
    /// <summary>
    /// Indexing Queue Extensions
    /// </summary>
    public static class IndexingQueueExtensions
    {
        /// <summary>
        /// Helper function, that registers all dependencies for RabbitMqIndexingQueue to Dependency Injcetion
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="queue">Queue name in RabbitMq where urls will be pushed to</param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMqIndexingQueue(this IServiceCollection services, IConfiguration configuration, string queue)
        {
            services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMq"));
            services.AddTransient<IIndexingQueue>((sp) =>
            {
                return new RabbitMqIndexingQueue(sp.GetService<IOptions<RabbitMqConfiguration>>(), queue);
            });
            return services;
        }
    }
}
