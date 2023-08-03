using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFeedSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var feedSettings = configuration
                    .GetRequiredSection(nameof(FeedSettings))
                    .Get<FeedSettings>()!;

            var feedArg = configuration["feed"] ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(feedArg))
            {
                feedSettings.Feed = feedArg;
            }

            services.AddSingleton(feedSettings);
        }
        public static void AddCrawlerSettings(this IServiceCollection services, IConfiguration configuration)
        {
            
            var crawlerSettings = configuration
                .GetRequiredSection(nameof(CrawlerSettings))
                .Get<CrawlerSettings>()!;

            var targetStatusArg = configuration["TargetStatus"] ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(targetStatusArg))
            {
                crawlerSettings.TargetStatus = targetStatusArg;
            }

            services.AddSingleton(crawlerSettings);
        }
    }
}
