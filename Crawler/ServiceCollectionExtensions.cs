using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFeedSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(
                configuration
                    .GetRequiredSection(nameof(FeedSettings))
                    .Get<FeedSettings>()!
                );
        }
        public static void AddCrawlerSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(
                configuration
                    .GetRequiredSection(nameof(CrawlerSettings))
                    .Get<CrawlerSettings>()!
                );
        }
    }
}
