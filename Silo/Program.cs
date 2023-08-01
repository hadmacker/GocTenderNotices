using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Silo
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .UseOrleans(silo =>
                {
                    silo.UseLocalhostClustering()
                        .ConfigureLogging(logging => logging.AddConsole());

                })
                .UseConsoleLifetime();
            using IHost host = builder.Build();

            await host.RunAsync();
        }
    }
}