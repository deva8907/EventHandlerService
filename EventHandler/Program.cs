using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventHandler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder();
            builder.ConfigureServices(services =>
            {
                services.AddLogging(configure => configure.AddConsole());
            });
            
            await builder.RunConsoleAsync();
        }
    }
}
