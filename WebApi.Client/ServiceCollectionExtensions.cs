using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTenderNoticesApiClient(
            this IServiceCollection services, 
            ConfigurationManager configuration)
        {
            services.AddSingleton(
                configuration
                    .GetRequiredSection(nameof(TenderNoticeClientSettings))
                    .Get<TenderNoticeClientSettings>()!
                );
            services.AddSingleton<ITenderNoticesApiClient, TenderNoticesApiClient>();
            return services;
        }
    }
}