using Microsoft.Extensions.Hosting;
using Orleans.Serialization;

namespace Crawler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
              .UseOrleansClient((ctx, client) =>
              {
                  if (ctx.HostingEnvironment.IsDevelopment())
                      client.UseLocalhostClustering();

                  client.Services.AddSerializer(serializerBuilder =>
                  {
                      serializerBuilder.AddJsonSerializer(
                           isSupported: type => type.Namespace.StartsWith("GocTenderNotices"));
                  });
              })
               .ConfigureLogging(logging => logging.AddConsole())
               .UseConsoleLifetime();

            using IHost host = builder.Build();
            await host.StartAsync();
        }
    }
}