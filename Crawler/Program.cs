using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Xml.Linq;
using WebApi.Client;
using Microsoft.Extensions.Configuration;
using WebApi.Contracts.DTO;
using System.Diagnostics;

namespace Crawler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // [Configuration](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration)
            builder.Configuration
                .AddJsonFile("appsettings.json")
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddHostedService<Worker>();
            builder.Services.AddCrawlerSettings(builder.Configuration);
            builder.Services.AddFeedSettings(builder.Configuration);
            builder.Services.AddTenderNoticesApiClient(builder.Configuration);

            using IHost host = builder.Build();
            host.Run();
        }
    }

    public class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<Worker> _logger;
        private readonly ITenderNoticesApiClient _tenderNoticeClient;
        private readonly FeedSettings _feedSettings;
        private readonly CrawlerSettings _crawlerSettings;

        public Worker(
            IHostApplicationLifetime lifetime,
            ILogger<Worker> logger, 
            FeedSettings feedSettings,
            CrawlerSettings crawlerSettings,
            ITenderNoticesApiClient tenderNoticeClient
            )
        {
            _lifetime = lifetime;
            _logger = logger;
            _tenderNoticeClient = tenderNoticeClient;
            _feedSettings = feedSettings;
            _crawlerSettings = crawlerSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (string.IsNullOrWhiteSpace(_feedSettings.Feed))
                return;
            await CrawlFeed(_feedSettings.Feed);
        }

        private async Task CrawlFeed(string rssFeedUrl)
        {
            var elapsed = new Stopwatch();
            elapsed.Start();
            try
            {
                using (XmlReader reader = XmlReader.Create(rssFeedUrl))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(reader);

                    if (!feed.Items.Any())
                    {
                        var feedLinks = feed.Links.Where(l => l.RelationshipType != "self");
                        foreach (var link in feedLinks)
                        {
                            await CrawlFeed(link.Uri.ToString());
                        }
                    }
                    else
                    {
                        // Get the RSS feed items
                        List<string> rssItems = new List<string>();
                        foreach (var item in feed.Items)
                        {
                            if(_feedSettings.ExcludedIdPrefixes.Any(prefix => item.Id.StartsWith(prefix))) {
                                // Skip item if its prefix begins with an excluded prefix.
                                continue;
                            }

                            var currentItem = new TenderNoticeDto
                            {
                                Id = item.Id,
                                Title =item.Title.Text,
                                Description = item.Summary.Text,
                                Link = item?.Links?.FirstOrDefault()?.Uri?.ToString() ?? "",
                                Status = _crawlerSettings.TargetStatus,
                                VisibleDate = DateTime.Now,
                            };
                            _logger.LogInformation($"{item.Title.Text} - {item.Content} - {item.Links}");
                            foreach (var extension in item.ElementExtensions)
                            {
                                if (extension.GetObject<XElement>() is XElement customElement)
                                {
                                    if(!customElement.HasElements)
                                    {
                                        _logger.LogInformation($"{customElement.Name}:{customElement?.Value}");
                                        if(customElement.Name.LocalName == "creator")
                                        {
                                            currentItem.Creator = customElement.Value;
                                        }
                                    }
                                    if(customElement.HasElements)
                                    {
                                        foreach (var ce in customElement.Elements())
                                        {
                                            _logger.LogInformation($"{ce.Name}:{ce?.Value}");
                                        }
                                    }
                                }
                            }
                            await _tenderNoticeClient.PostTenderNotice(currentItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching RSS feed: " + ex.Message);
            }

            _logger.LogInformation($"Worker process complete in {elapsed.Elapsed.ToString()}");
            _lifetime.StopApplication();
        }
    }
}