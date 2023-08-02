using Microsoft.Extensions.Hosting;
using Orleans.Serialization;

namespace Silo
{
    public class Program
    {
        private const string AzureEnvVarName = "goctnorleans";
        static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .UseOrleans(silo =>
                {
                    silo.UseLocalhostClustering();

                    var goctnorleans = Environment.GetEnvironmentVariable(AzureEnvVarName);

                    if(string.IsNullOrWhiteSpace(goctnorleans))
                    {
                        Console.WriteLine($"Environment Variable {AzureEnvVarName} not found. using in-memory storage");
                        silo.AddMemoryGrainStorage("tenderNoticesStore");
                    } 
                    else
                    {
                        silo.AddAzureTableGrainStorage(
                        name: "tenderNoticesStore",
                        configureOptions: options =>
                        {
                            options.ConfigureTableServiceClient(goctnorleans);
                            options.TableName = $"tenderNotices";
                        }
                        );
                    }
                    silo.Services
                        .AddSerializer(
                            serializationBuilder =>
                                serializationBuilder.AddJsonSerializer(
                                    isSupported: type => type.Namespace.StartsWith("GocTenderNotices")));
                })
                .UseConsoleLifetime();
            using IHost host = builder.Build();

            await host.RunAsync();
        }
    }
}