using EventHandler.EventProcessors;
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
                services.Configure<KafkaConfiguration>(options =>
                {
                    options.ServerUrl = "localhost:29092";
                    options.Topics = ["Orders", "Payments", "Shipping"];
                });
                ConfigureEventProcessors(services);
                services.AddHostedService<KafkaProducerService>();
                services.AddHostedService<KafkaConsumerService>();
            });
            
            await builder.RunConsoleAsync();
        }

        private static void ConfigureEventProcessors(IServiceCollection services)
        {
            services.AddSingleton<OrderCreatedEventProcessor>();
            services.AddSingleton<OrderShippedEventProcessor>();
            services.AddSingleton<PaymentWithCashCompletedEventProcessor>();
            services.AddSingleton(serviceProvider =>
            {
                var factory = new EventProcessorFactory();
                factory.Register(serviceProvider.GetRequiredService<OrderCreatedEventProcessor>());
                factory.Register(serviceProvider.GetRequiredService<OrderShippedEventProcessor>());
                factory.Register(serviceProvider.GetRequiredService<PaymentWithCashCompletedEventProcessor>());

                return factory;
            });
        }
    }
}
