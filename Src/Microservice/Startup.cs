using System.Threading.Tasks;
using Jay.FuncHubDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jay.FuncHubDemo
{
    internal static class Startup
    {
        public const string HttpHubName = "http-hub";
        public const string AlphaHubName = "alpha-hub";
        public const string HttpHubAlphaConsumer = "alpha";
        public const string HttpHubBetaConsumer = "beta";
        public const string HttpHubReadWriteConnection = "HttpHubReadWriteConnection";
        public const string AlphaHubWriteConnection = "AlphaHubWriteConnection";

        public static async Task Main()
        {

            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureHostConfiguration(hostBuilder=>
                {
                    //add ability to preface env variables for netcoreapp7 specific settings.
                    // i.e. NETCOREAPP7_ENVIRONMENT Development, etc.
                    hostBuilder.AddEnvironmentVariables("NETCOREAPP7_");
                })
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services.AddTransient<IMyService, MyService>();
                }).Build();
            await host.RunAsync();
        }
    }
}

    