using GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Serialization;

namespace Client
{
    public class Program
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

            var client = host.Services.GetRequiredService<IClusterClient>();
            var tenderNotice = client.GetGrain<ITenderNotice>("grain1");
            await tenderNotice.ProcessUpdate(new GocTenderNotices.Contracts.State.TenderNoticeState
            {
                FeedGuid = "grain1",
                Title = "TitleValue",
                Link = "LinkValue",
                Description = "DescriptionValue",
                VisibleDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Creator = "CreatorValue",
            }, GocTenderNotices.Contracts.State.ProcurementStatus.Active);

            await host.StopAsync();
        }
    }
}