using Examples.Consumer.Consumers;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit.Logging;

namespace Runner
{
    public class Program
    {
        static bool? _isRunningInContainer;

        static bool IsRunningInContainer =>
            _isRunningInContainer ??= bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) && inContainer;

        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddDelayedMessageScheduler();

                        x.SetKebabCaseEndpointNameFormatter();

                        // By default, sagas are in-memory, but should be changed to a durable
                        // saga repository.
                        x.SetInMemorySagaRepositoryProvider();

                        var consumerAssemblies = Assembly.GetAssembly(typeof(DummyConsumer));
                        x.AddConsumers(consumerAssemblies);

                        var entryAssembly = Assembly.GetEntryAssembly();
                        x.AddSagaStateMachines(entryAssembly);
                        x.AddSagas(entryAssembly);
                        x.AddActivities(entryAssembly);

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            if (IsRunningInContainer)
                                cfg.Host("rabbitmq");
                            
                            cfg.UseDelayedMessageScheduler();

                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });
    }
}
