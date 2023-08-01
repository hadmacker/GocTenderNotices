using GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Client
{
    public class Program
    {
        static async void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .UseOrleansClient(client =>
                {
                    client.UseLocalhostClustering();
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .UseConsoleLifetime();

            using IHost host = builder.Build();
            await host.StartAsync();

            var client = host.Services.GetRequiredService<IClusterClient>();
            var tenderNotice = client.GetGrain<ITenderNotice>("grain1");
            await tenderNotice.ProcessUpdate(new GocTenderNotices.Contracts.State.TenderNoticeState
            {
                FeedGuid = "grain1"
            }, GocTenderNotices.Contracts.State.ProcurementStatus.Active);

            await host.StopAsync();
        }
    }
}