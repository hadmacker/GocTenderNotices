using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Xml.Linq;

namespace Crawler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            using IHost host = builder.Build();
            host.Run();
        }
    }

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string rssFeedUrl = "file:///C:/code/hadmacker/GocTenderNotices/Documents/procurement-data/feed/local/active.rss";
            CrawlFeed(rssFeedUrl);
        }

        private void CrawlFeed(string rssFeedUrl)
        {
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
                            CrawlFeed(link.Uri.ToString());
                        }
                    }
                    else
                    {
                        // Get the RSS feed items
                        List<string> rssItems = new List<string>();
                        foreach (var item in feed.Items)
                        {
                            _logger.LogInformation($"{item.Title.Text} - {item.Content} - {item.Links}");
                            foreach (var extension in item.ElementExtensions)
                            {
                                if (extension.GetObject<XElement>() is XElement customElement)
                                {
                                    if(!customElement.HasElements)
                                    {
                                        _logger.LogInformation($"{customElement.Name}:{customElement?.Value}");
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching RSS feed: " + ex.Message);
            }
        }
    }
}