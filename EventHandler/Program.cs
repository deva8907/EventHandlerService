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
                    options.MaxRetry = 3;
                });
                ConfigureEventProcessors(services);
                services.AddSingleton<KafkaProducerClient>();
                services.AddHostedService<KafkaProducerService>();
                services.AddHostedService<KafkaConsumerService>();
            });
            
            await builder.RunConsoleAsync();
        }

        private static void ConfigureEventProcessors(IServiceCollection services)
        {
            services.AddSingleton<OrderCreatedEventProcessor>();
            services.AddSingleton<OrderShippedEventProcessor>();
            services.AddSingleton<PaymentCompletedEventProcessor>();
            services.AddSingleton(serviceProvider =>
            {
                var factory = new EventProcessorFactory();
                factory.Register("Orders", "OrderCreated", serviceProvider.GetRequiredService<OrderCreatedEventProcessor>());
                factory.Register("Shipping", "OrderShipped", serviceProvider.GetRequiredService<OrderShippedEventProcessor>());
                factory.Register("Payments", "PaymentCompleted", serviceProvider.GetRequiredService<PaymentCompletedEventProcessor>());

                return factory;
            });
        }
    }
}
