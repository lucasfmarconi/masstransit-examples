using MassTransit;

namespace Examples.Consumer.Consumers;

public class DummyConsumerDefinition : ConsumerDefinition<DummyConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<DummyConsumer> consumerConfigurator)
    {
        endpointConfigurator.ConcurrentMessageLimit = 1;
        //endpointConfigurator.UseMessageRetry(r =>
        //{
        //    r.Interval(5, TimeSpan.FromSeconds(5));
        //});
    }
}